using Accreditation.Application.Abstractions.EventBusService;
using MediatR;

namespace Accreditation.Application.AccreditationInstancesEnvironmentStandardsResultEvents.AddEvent;

public class AccreditationInstancesEnvironmentStandardsResultEvent : IntegrationEvent, INotification
{
    public Guid AccreditationInstanceGUID { get; private set; }
    public Guid EtebarDoreGuid { get; private set; }

    public AccreditationInstancesEnvironmentStandardsResultEvent(Guid etebarDoreGuid,
                                                                 Guid accreditationInstanceGUID)
    {
        EtebarDoreGuid = etebarDoreGuid;
        AccreditationInstanceGUID = accreditationInstanceGUID;
    }
}