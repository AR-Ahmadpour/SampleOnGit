using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.AccInstanceMehvars.AddEvent;
using Accreditation.Application.Common.Interfaces.Persistence.accInstanceMehvarEnvironmentStandardsResults;
using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceMehvars;
using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceZirMehvars;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using Accreditation.Domain.AccInstanceZirMehvars.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accreditation.Application.AccInstanceZirMehvars.AddEvent;
public class AccInstanceZirMehvarCreatedEventHandler : INotificationHandler<AccInstanceZirMehvarCreatedEvent>
{
    private readonly IAccInstanceZirMehvarRepository _accInstanceZirMehvarRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IZirMehvarRepository _zirMehvarRepository;
    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;
    private readonly IAccInstanceMehvarRepository _accInstanceMehvarRepository;
    private readonly IAccInstanceMehvarEnvironmentStandardsResultRepository _accInstanceMehvarEnvironmentStandardsResultRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<AccInstanceZirMehvarCreatedEventHandler> _logger;


    public AccInstanceZirMehvarCreatedEventHandler(IAccInstanceZirMehvarRepository accInstanceZirMehvarRepository,
         ILogger<AccInstanceZirMehvarCreatedEventHandler> logger,
                                         IUnitOfWork unitOfWork,
                                         IZirMehvarRepository zirMehvarRepository,
                                         IAccreditationInstanceRepository accreditationInstanceRepository,
                                         IAccInstanceMehvarRepository accInstanceMehvarRepository,
                                         IAccInstanceMehvarEnvironmentStandardsResultRepository accInstanceMehvarEnvironmentStandardsResultRepository,
                                         IMediator mediator)
    {
        _accInstanceZirMehvarRepository = accInstanceZirMehvarRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _zirMehvarRepository = zirMehvarRepository;
        _accreditationInstanceRepository = accreditationInstanceRepository;
        _accInstanceMehvarRepository = accInstanceMehvarRepository;
        _accInstanceMehvarEnvironmentStandardsResultRepository = accInstanceMehvarEnvironmentStandardsResultRepository;
        _mediator = mediator;
    }

    public async Task Handle(AccInstanceZirMehvarCreatedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var zirmehvars = await _zirMehvarRepository.GetSelectListAsync(notification.EtebarDoreGuid);
            foreach (var zirmehvar in zirmehvars)
            {
                if (notification.MehvarGuid == zirmehvar.MehvarGUID)
                {
                    var accInstanceZirMehvar = AccInstanceZirMehvar.Create(notification.AccreditationInstanceGUID,
                                                                 notification.AccInstanceMehvarGUID,
                                                                 zirmehvar.MehvarGUID);
                    await _accInstanceZirMehvarRepository.Add(accInstanceZirMehvar);

                    //publish event
                    await _mediator.Publish(new AccInstanceZirMehvarEnvironmentStandardsResultEvent(accInstanceZirMehvar.GUID,
                                                                                                   notification.EtebarDoreGuid,
                                                                                                   notification.AccreditationInstanceGUID));

                    await _mediator.Publish(new AccInstanceStandardCreatedEvent(notification.AccreditationInstanceGUID,
                                                                                accInstanceZirMehvar.GUID,
                                                                                zirmehvar.GUID,
                                                                                notification.EtebarDoreGuid), cancellationToken);
                }
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
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            throw new Exception(ex.Message);
        }
    }
}
