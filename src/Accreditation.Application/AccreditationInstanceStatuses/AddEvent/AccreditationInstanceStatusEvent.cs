using Accreditation.Application.Abstractions.EventBusService;
using MediatR;

namespace Accreditation.Application.AccreditationInstanceStatuses.AddEvent;

public class AccreditationInstanceStatusesEvent : IntegrationEvent, INotification
{
    public Guid AccreditationInstanceGUID { get; private set; }

    public AccreditationInstanceStatusesEvent( Guid accreditationInstanceGUID)
    {
        AccreditationInstanceGUID = accreditationInstanceGUID;
    }
}