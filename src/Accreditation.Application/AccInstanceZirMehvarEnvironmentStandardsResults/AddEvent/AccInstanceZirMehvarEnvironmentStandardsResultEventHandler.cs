using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.AccInstanceMehvars.AddEvent;
using Accreditation.Application.Common.Interfaces.Persistence.accInstanceMehvarEnvironmentStandardsResults;
using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceMehvars;
using Accreditation.Application.Common.Interfaces.Persistence.accInstanceZirMehvarEnvironmentStandardsResults;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.EnvironmentStandards;
using Accreditation.Domain.AccInstanceZirMehvarEnvironmentStandardsResults.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accreditation.Application.AccInstanceZirMehvars.AddEvent;
public class AccInstanceZirMehvarEnvironmentStandardsResultEventHandler : INotificationHandler<AccInstanceZirMehvarEnvironmentStandardsResultEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccInstanceMehvarRepository _accInstanceMehvarRepository;
    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;
    private readonly IEnvironmentStandardRepository _environmentStandardRepository;
    private readonly IAccInstanceMehvarEnvironmentStandardsResultRepository _accInstanceMehvarEnvironmentStandardsResultRepository;
    private readonly IAccInstanceZirMehvarEnvironmentStandardsResultRepository _accInstanceZirMehvarEnvironmentStandardsResultRepository;
    private readonly ILogger<AccInstanceZirMehvarEnvironmentStandardsResultEventHandler> _logger;

    public AccInstanceZirMehvarEnvironmentStandardsResultEventHandler(IUnitOfWork unitOfWork,
        ILogger<AccInstanceZirMehvarEnvironmentStandardsResultEventHandler> logger,
                                                                   IAccInstanceMehvarRepository accInstanceMehvarRepository,
                                                                   IAccreditationInstanceRepository accreditationInstanceRepository,
                                                                   IEnvironmentStandardRepository environmentStandardRepository,
                                                                   IAccInstanceMehvarEnvironmentStandardsResultRepository accInstanceMehvarEnvironmentStandardsResultRepository,
                                                                   IAccInstanceZirMehvarEnvironmentStandardsResultRepository accInstanceZirMehvarEnvironmentStandardsResultRepository)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _accInstanceMehvarRepository = accInstanceMehvarRepository;
        _accreditationInstanceRepository = accreditationInstanceRepository;
        _environmentStandardRepository = environmentStandardRepository;
        _accInstanceMehvarEnvironmentStandardsResultRepository = accInstanceMehvarEnvironmentStandardsResultRepository;
        _accInstanceZirMehvarEnvironmentStandardsResultRepository = accInstanceZirMehvarEnvironmentStandardsResultRepository;
    }

    public async Task Handle(AccInstanceZirMehvarEnvironmentStandardsResultEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var environmentStandards = await _environmentStandardRepository.GetSelectListAsync(notification.EtebarDoreGuid);

            //environmentStandards.ForEach(environmentStandardsritem =>
            //{
            foreach (var environmentStandardsritem in environmentStandards)
            {
                var accInstanceMehvarResult = AccInstanceZirMehvarEnvironmentStandardsResult.Create(notification.AccreditationInstanceZirMehvarGUID,
                                                                                                 environmentStandardsritem.Guid);
                _accInstanceZirMehvarEnvironmentStandardsResultRepository.Add(accInstanceMehvarResult);
                //});
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();

            var accInstanceMehvarResults = await _accInstanceMehvarEnvironmentStandardsResultRepository.GetListAccInstanceMehvarAsync(notification.AccreditationInstanceGUID);
            if (accInstanceMehvarResults != null)
            {
                foreach (var accInstanceMehvarResult in accInstanceMehvarResults)
                {
                    _accInstanceMehvarEnvironmentStandardsResultRepository.Delete(accInstanceMehvarResult);
                }
            }
            var accInstanceMehvarz = await _accInstanceMehvarRepository.GetListAccInstanceMehvarAsync(notification.AccreditationInstanceGUID);
            if (accInstanceMehvarz != null)
            {
                foreach (var accInstanceMehvar in accInstanceMehvarz)
                {
                    _accInstanceMehvarRepository.Delete(accInstanceMehvar);
                }
            }
            var evaluation = await _accreditationInstanceRepository.FindAsync(notification.AccreditationInstanceGUID);
            if (evaluation != null)
            {
                _accreditationInstanceRepository.Delete(evaluation);
            }
            throw new Exception(ex.Message);
        }
    }
}