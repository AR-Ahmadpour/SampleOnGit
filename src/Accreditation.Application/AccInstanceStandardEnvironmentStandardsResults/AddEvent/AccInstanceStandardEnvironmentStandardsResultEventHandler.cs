using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.AccInstanceMehvars.AddEvent;
using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceMehvars;
using Accreditation.Application.Common.Interfaces.Persistence.accInstanceStandardEnvironmentStandardsResults;
using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceZirMehvars;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.EnvironmentStandards;
using Accreditation.Domain.AccInstanceStandardEnvironmentStandardsResults.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accreditation.Application.AccInstanceStandardEnvironmentStandardsResults.AddEvent;
public class AccInstanceZirMehvarEnvironmentStandardsResultEventHandler : INotificationHandler<AccInstanceStandardEnvironmentStandardsResultEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccInstanceZirMehvarRepository _accInstanceZirMehvarRepository;
    private readonly IAccInstanceMehvarRepository _accInstanceMehvarRepository;
    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;
    private readonly IEnvironmentStandardRepository _environmentStandardRepository;
    private readonly IAccInstanceStandardEnvironmentStandardsResultRepository _accInstanceStandardEnvironmentStandardsResultRepository;
    private readonly ILogger<AccInstanceZirMehvarEnvironmentStandardsResultEventHandler> _logger;


    public AccInstanceZirMehvarEnvironmentStandardsResultEventHandler(IUnitOfWork unitOfWork,
        ILogger<AccInstanceZirMehvarEnvironmentStandardsResultEventHandler> logger,
                                                                   IAccInstanceZirMehvarRepository accInstanceZirMehvarRepository,
                                                                   IAccInstanceMehvarRepository accInstanceMehvarRepository,
                                                                   IAccreditationInstanceRepository accreditationInstanceRepository,
                                                                   IEnvironmentStandardRepository environmentStandardRepository,
                                                                   IAccInstanceStandardEnvironmentStandardsResultRepository accInstanceStandardEnvironmentStandardsResultRepository)
    {
        _unitOfWork = unitOfWork;
        _logger=logger;
        _accInstanceZirMehvarRepository = accInstanceZirMehvarRepository;
        _accInstanceMehvarRepository = accInstanceMehvarRepository;
        _accreditationInstanceRepository = accreditationInstanceRepository;
        _environmentStandardRepository = environmentStandardRepository;
        _accInstanceStandardEnvironmentStandardsResultRepository = accInstanceStandardEnvironmentStandardsResultRepository;
    }

    public async Task Handle(AccInstanceStandardEnvironmentStandardsResultEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var environmentStandards = await _environmentStandardRepository.GetSelectListAsync(notification.EtebarDoreGuid);

            environmentStandards.ForEach(environmentStandardsritem =>
            {
                var accInstanceMehvarResult = AccInstanceStandardEnvironmentStandardsResult.Create(notification.AccreditationInstanceStandardGUID,
                                                                                                   environmentStandardsritem.Guid);
                _accInstanceStandardEnvironmentStandardsResultRepository.Add(accInstanceMehvarResult);
            });
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();

            var accInstanceZirMehvarz = await _accInstanceZirMehvarRepository.GetListAccInstanceZirMehvarAsync(notification.AccreditationInstanceGUID);
            if (accInstanceZirMehvarz != null)
            {
                foreach (var accInstanceZirMehvar in accInstanceZirMehvarz)
                {
                    _accInstanceZirMehvarRepository.Delete(accInstanceZirMehvar);
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
            throw new Exception( ex.Message);
        }
    }
}