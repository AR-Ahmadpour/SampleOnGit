using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.AccInstanceMehvars.AddEvent;
using Accreditation.Application.Common.Interfaces.Persistence.accInstanceMehvarEnvironmentStandardsResults;
using Accreditation.Application.Common.Interfaces.Persistence.accInstanceZirMehvarEnvironmentStandardsResults;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstancesEnvironmentStandardsResults;
using Accreditation.Application.Common.Interfaces.Persistence.EnvironmentStandards;
using Accreditation.Domain.AccreditationInstancesEnvironmentStandardsResults.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accreditation.Application.AccreditationInstancesEnvironmentStandardsResultEvents.AddEvent;
public class AccreditationInstancesEnvironmentStandardsResultEventHandler : INotificationHandler<AccreditationInstancesEnvironmentStandardsResultEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;
    private readonly IEnvironmentStandardRepository _environmentStandardRepository;
    private readonly IAccreditationInstancesEnvironmentStandardsResultRepository _accrInstancesEnvironmentStandardsResultRepository;
    private readonly IAccInstanceZirMehvarEnvironmentStandardsResultRepository _accInstanceZirMehvarEnvironmentStandardsResultRepository;
    private readonly IAccInstanceMehvarEnvironmentStandardsResultRepository _accInstanceMehvarEnvironmentStandardsResultRepository;
    private readonly ILogger<AccreditationInstancesEnvironmentStandardsResultEventHandler> _logger;

    public AccreditationInstancesEnvironmentStandardsResultEventHandler(IUnitOfWork unitOfWork,
         ILogger <AccreditationInstancesEnvironmentStandardsResultEventHandler>logger,
                                                                   IAccreditationInstanceRepository accreditationInstanceRepository,
                                                                   IEnvironmentStandardRepository environmentStandardRepository,
                                                                   IAccreditationInstancesEnvironmentStandardsResultRepository accrInstancesEnvironmentStandardsResultRepository,
                                                                   IAccInstanceZirMehvarEnvironmentStandardsResultRepository accInstanceZirMehvarEnvironmentStandardsResultRepository,
                                                                   IAccInstanceMehvarEnvironmentStandardsResultRepository accInstanceMehvarEnvironmentStandardsResultRepository)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _accreditationInstanceRepository = accreditationInstanceRepository;
        _environmentStandardRepository = environmentStandardRepository;
        _accrInstancesEnvironmentStandardsResultRepository = accrInstancesEnvironmentStandardsResultRepository;
        _accInstanceZirMehvarEnvironmentStandardsResultRepository = accInstanceZirMehvarEnvironmentStandardsResultRepository;
        _accInstanceMehvarEnvironmentStandardsResultRepository = accInstanceMehvarEnvironmentStandardsResultRepository;
    }

    public async Task Handle(AccreditationInstancesEnvironmentStandardsResultEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var environmentStandards =  _environmentStandardRepository.GetGuidList(notification.EtebarDoreGuid);

            foreach (var environmentStandardsritem in environmentStandards)
            {
                var accInstanceEnvironmentStandardResult = AccreditationInstancesEnvironmentStandardsResult.Create(notification.AccreditationInstanceGUID,
                                                                                                                                       environmentStandardsritem);
                _accrInstancesEnvironmentStandardsResultRepository.Add(accInstanceEnvironmentStandardResult);

            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();

            var accInstanceZirMehvarResults = await _accInstanceZirMehvarEnvironmentStandardsResultRepository.GetListAccInstanceZirMehvarAsync(notification.AccreditationInstanceGUID);
            if (accInstanceZirMehvarResults != null)
            {
                foreach (var accInstanceMehvarResult in accInstanceZirMehvarResults)
                {
                    _accInstanceZirMehvarEnvironmentStandardsResultRepository.Delete(accInstanceMehvarResult);
                }
            }
            var accInstanceMehvarResults = await _accInstanceMehvarEnvironmentStandardsResultRepository.GetListAccInstanceMehvarAsync(notification.AccreditationInstanceGUID);
            if (accInstanceMehvarResults != null)
            {
                foreach (var accInstanceMehvarResult in accInstanceMehvarResults)
                {
                    _accInstanceMehvarEnvironmentStandardsResultRepository.Delete(accInstanceMehvarResult);
                }
            }

            var evaluation = await _accreditationInstanceRepository.FindAsync(notification.AccreditationInstanceGUID);
            if (evaluation != null)
            {
                _accreditationInstanceRepository.Delete(evaluation);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogError(ex.Message);

        }
    }
}