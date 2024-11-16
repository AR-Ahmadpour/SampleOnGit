using Accreditation.Application.Abstractions.EventBusService;
using MediatR;

namespace Accreditation.Application.AccInstanceZirMehvars.AddEvent;
public class AccInstanceStandardCreatedEvent : IntegrationEvent, INotification
{
    public Guid AccreditationInstanceGUID { get; private set; }
    public Guid AccInstanceZirMehvarGUID { get; private set; }
    public Guid EtebarDoreGuid { get; private set; }
    public Guid ZirMehvarGUID { get; private set; }

    public AccInstanceStandardCreatedEvent(Guid accreditationInstanceGUID,
                                           Guid accInstanceZirMehvarGUID,
                                           Guid zirMehvarGUID,
                                           Guid etebarDoreGuid)
    {
        AccreditationInstanceGUID = accreditationInstanceGUID;
        AccInstanceZirMehvarGUID = accInstanceZirMehvarGUID;
        ZirMehvarGUID = zirMehvarGUID;
        EtebarDoreGuid = etebarDoreGuid;
    }
}
