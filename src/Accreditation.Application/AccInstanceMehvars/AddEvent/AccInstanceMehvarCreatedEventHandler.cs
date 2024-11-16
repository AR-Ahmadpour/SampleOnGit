using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.AccInstanceZirMehvars.AddEvent;
using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceMehvars;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.EnvironmentStandards;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Domain.AccInstanceMehvars.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accreditation.Application.AccInstanceMehvars.AddEvent;
public class AccInstanceMehvarCreatedEventHandler : INotificationHandler<AccInstanceMehvarCreatedEvent>
{
    private readonly IAccInstanceMehvarRepository _accInstanceMehvarRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMehvarRepository _mehvarRepository;
    private readonly IMediator _mediator;
    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;
    private readonly IEnvironmentStandardRepository _environmentStandardRepository;
    private readonly ILogger<AccInstanceMehvarCreatedEventHandler> _logger;


    public AccInstanceMehvarCreatedEventHandler(IAccInstanceMehvarRepository accInstanceMehvarRepository,
                                         IUnitOfWork unitOfWork,
                                         IMehvarRepository mehvarRepository,
                                         IMediator mediator,
                                         ILogger<AccInstanceMehvarCreatedEventHandler> logger,
                                         IAccreditationInstanceRepository accreditationInstanceRepository,
                                         IEnvironmentStandardRepository environmentStandardRepository)
    {
        _accInstanceMehvarRepository = accInstanceMehvarRepository;
        _unitOfWork = unitOfWork;
        _mehvarRepository = mehvarRepository;
        _mediator = mediator;
        _logger = logger;
        _accreditationInstanceRepository = accreditationInstanceRepository;
        _environmentStandardRepository = environmentStandardRepository;

    }


    public async Task Handle(AccInstanceMehvarCreatedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var mehvars = await _mehvarRepository.GetSelectListAsync(notification.EtebarDoreGuid);
            var environmentStandards = _environmentStandardRepository.GetGuidList(notification.EtebarDoreGuid);

            foreach (var mehvarGuid in mehvars)
            {
                // Create the new AccInstanceMehvar entity
                var accInstanceMehvar = AccInstanceMehvar.Create(notification.AccreditationInstanceGUID, mehvarGuid);
              
                // Detach any existing entity with the same GUID before adding the new instance
                var existingEntity = _accInstanceMehvarRepository.Find(accInstanceMehvar.GUID);
                if (existingEntity != null)
                {
                    // Now add the new entity
                    await _accInstanceMehvarRepository.Add(accInstanceMehvar);

                    // Publish events

                    await _mediator.Publish(new AccInstanceMehvarEnvironmentStandardsResultEvent(accInstanceMehvar.GUID,
                                                                                               notification.EtebarDoreGuid,
                                                                                               notification.AccreditationInstanceGUID                                                                                               
                                                                                               , environmentStandards));

                    await _mediator.Publish(new AccInstanceZirMehvarCreatedEvent(notification.AccreditationInstanceGUID,
                                                                                 accInstanceMehvar.GUID,
                                                                                 notification.EtebarDoreGuid,
                                                                                 mehvarGuid)
                                            , cancellationToken);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();

            var evaluation = await _accreditationInstanceRepository.FindAsync(notification.AccreditationInstanceGUID);
            if (evaluation != null)
            {
                _accreditationInstanceRepository.Delete(evaluation);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            throw new Exception(ex.Message);
        }
    }




    //public async Task Handle(AccInstanceMehvarCreatedEvent notification, CancellationToken cancellationToken)
    //{
    //    try
    //    {
    //        var mehvars = await _mehvarRepository.GetSelectListAsync(notification.EtebarDoreGuid);
    //        foreach (var mehvarGuid in mehvars)
    //        {
    //            var accInstanceMehvar = AccInstanceMehvar.Create(notification.AccreditationInstanceGUID, mehvarGuid);
    //            await _accInstanceMehvarRepository.Add(accInstanceMehvar);

    //            //Publish event
    //            await _mediator.Publish(new AccInstanceMehvarEnvironmentStandardsResultEvent(accInstanceMehvar.GUID,
    //                                                                                       notification.EtebarDoreGuid,
    //                                                                                       notification.AccreditationInstanceGUID));

    //            await _mediator.Publish(new AccInstanceZirMehvarCreatedEvent(notification.AccreditationInstanceGUID,
    //                                                                         accInstanceMehvar.GUID,
    //                                                                         notification.EtebarDoreGuid,
    //                                                                         mehvarGuid), cancellationToken);
    //        }

    //        await _unitOfWork.SaveChangesAsync(cancellationToken);

    //    }
    //    catch (Exception)
    //    {
    //        _unitOfWork.Rollback();

    //        var evaluation = await _accreditationInstanceRepository.FindAsync(notification.AccreditationInstanceGUID);
    //        if (evaluation != null)
    //        {
    //            _accreditationInstanceRepository.Delete(evaluation);
    //            await _unitOfWork.SaveChangesAsync(cancellationToken);
    //        }
    //        await _unitOfWork.SaveChangesAsync(cancellationToken);
    //        throw;
    //    }
    //}
}