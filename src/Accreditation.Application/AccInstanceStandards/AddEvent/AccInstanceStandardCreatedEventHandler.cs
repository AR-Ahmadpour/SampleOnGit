using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.AccInstanceStandardEnvironmentStandardsResults.AddEvent;
using Accreditation.Application.Common.Interfaces.Persistence.accInstanceMehvarEnvironmentStandardsResults;
using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceMehvars;
using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceStandards;
using Accreditation.Application.Common.Interfaces.Persistence.accInstanceZirMehvarEnvironmentStandardsResults;
using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceZirMehvars;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.Standards;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using Accreditation.Domain.AccInstanceStandards.Entities;
using MediatR;

namespace Accreditation.Application.AccInstanceZirMehvars.AddEvent;
public class AccInstanceStandardCreatedEventHandler : INotificationHandler<AccInstanceStandardCreatedEvent>
{
    private readonly IAccInstanceZirMehvarRepository _accInstanceZirMehvarRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IZirMehvarRepository _zirMehvarRepository;
    private readonly IStandardRepository _standardRepository;
    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;
    private readonly IAccInstanceStandardRepository _accInstanceStandardRepository;
    private readonly IAccInstanceMehvarRepository _accInstanceMehvarRepository;
    private readonly IMediator _mediator;
    private readonly IAccInstanceZirMehvarEnvironmentStandardsResultRepository _accInstanceZirMehvarEnvironmentStandardsResultRepository;
    private readonly IAccInstanceMehvarEnvironmentStandardsResultRepository _accInstanceMehvarEnvironmentStandardsResultRepository;

    public AccInstanceStandardCreatedEventHandler(IAccInstanceZirMehvarRepository accInstanceZirMehvarRepository,
                                         IUnitOfWork unitOfWork,
                                         IZirMehvarRepository zirMehvarRepository,
                                         IStandardRepository standardRepository,
                                         IAccreditationInstanceRepository accreditationInstanceRepository,
                                         IAccInstanceStandardRepository accInstanceStandardRepository,
                                         IAccInstanceMehvarRepository accInstanceMehvarRepository,
                                         IMediator mediator,
                                         IAccInstanceZirMehvarEnvironmentStandardsResultRepository accInstanceZirMehvarEnvironmentStandardsResultRepository,
                                         IAccInstanceMehvarEnvironmentStandardsResultRepository accInstanceMehvarEnvironmentStandardsResultRepository)
    {
        _accInstanceZirMehvarRepository = accInstanceZirMehvarRepository;
        _unitOfWork = unitOfWork;
        _zirMehvarRepository = zirMehvarRepository;
        _standardRepository = standardRepository;
        _accreditationInstanceRepository = accreditationInstanceRepository;
        _accInstanceStandardRepository = accInstanceStandardRepository;
        _accInstanceMehvarRepository = accInstanceMehvarRepository;
        _mediator = mediator;
        _accInstanceZirMehvarEnvironmentStandardsResultRepository = accInstanceZirMehvarEnvironmentStandardsResultRepository;
        _accInstanceMehvarEnvironmentStandardsResultRepository = accInstanceMehvarEnvironmentStandardsResultRepository;
    }

    public async Task Handle(AccInstanceStandardCreatedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var standards = await _standardRepository.GetSelectListAsync(notification.EtebarDoreGuid);
            foreach (var standard in standards)
            {
                if (standard.ZirMehvarGUID == notification.ZirMehvarGUID)
                {
                    var accInstancetandard = AccInstanceStandard.Create(notification.AccreditationInstanceGUID,
                                                                 notification.AccInstanceZirMehvarGUID,
                                                                 standard.GUID);
                    await _accInstanceStandardRepository.Add(accInstancetandard);

                    //publish event 
                    await _mediator.Publish(new AccInstanceStandardEnvironmentStandardsResultEvent(accInstancetandard.GUID,
                                                                                                   notification.EtebarDoreGuid,
                                                                                                   notification.AccreditationInstanceGUID));
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
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
            var accreditationInstance = await _accreditationInstanceRepository.FindAsync(notification.AccreditationInstanceGUID);
            if (accreditationInstance != null)
            {
                _accreditationInstanceRepository.Delete(accreditationInstance);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            throw;
        }
    }
}
