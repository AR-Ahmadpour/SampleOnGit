using Accreditation.Application.Abstractions.EventBusService;
using MediatR;

namespace Accreditation.Application.AccInstanceZirMehvars.AddEvent;
public class AccInstanceZirMehvarCreatedEvent : IntegrationEvent, INotification
{
    public Guid AccreditationInstanceGUID { get; private set; }
    public Guid AccInstanceMehvarGUID { get; private set; }
    public Guid EtebarDoreGuid { get; private set; }
    public Guid MehvarGuid { get; private set; }

    public AccInstanceZirMehvarCreatedEvent(Guid accreditationInstanceGUID,
                                            Guid accInstanceMehvarGUID,
                                            Guid etebarDoreGuid,
                                            Guid mehvarGuid)
    {
        AccreditationInstanceGUID = accreditationInstanceGUID;
        AccInstanceMehvarGUID = accInstanceMehvarGUID;
        EtebarDoreGuid = etebarDoreGuid;
        MehvarGuid = mehvarGuid;

    }
}
