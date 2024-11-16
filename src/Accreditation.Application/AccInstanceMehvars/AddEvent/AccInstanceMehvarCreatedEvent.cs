using Accreditation.Application.Abstractions.EventBusService;
using MediatR;

namespace Accreditation.Application.AccInstanceMehvars.AddEvent;

public class AccInstanceMehvarCreatedEvent : IntegrationEvent, INotification
{
    public Guid AccreditationInstanceGUID { get; private set; }
    public Guid EtebarDoreGuid { get; private set; }

    public AccInstanceMehvarCreatedEvent(Guid accreditationInstanceGUID,
                                         Guid etebarDoreGuid)
    {
        AccreditationInstanceGUID = accreditationInstanceGUID;
        EtebarDoreGuid = etebarDoreGuid;
    }
}