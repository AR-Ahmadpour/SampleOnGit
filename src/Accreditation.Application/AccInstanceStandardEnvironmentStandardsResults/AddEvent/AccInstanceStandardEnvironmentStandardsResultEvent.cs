using Accreditation.Application.Abstractions.EventBusService;
using MediatR;

namespace Accreditation.Application.AccInstanceStandardEnvironmentStandardsResults.AddEvent;

public class AccInstanceStandardEnvironmentStandardsResultEvent : IntegrationEvent, INotification
{
    public Guid AccreditationInstanceStandardGUID { get; private set; }
    public Guid AccreditationInstanceGUID { get; private set; }
    public Guid EtebarDoreGuid { get; private set; }

    public AccInstanceStandardEnvironmentStandardsResultEvent(Guid accreditationInstanceStandardGUID,
                                                            Guid etebarDoreGuid,
                                                            Guid accreditationInstanceGUID)
    {
        AccreditationInstanceStandardGUID = accreditationInstanceStandardGUID;
        EtebarDoreGuid = etebarDoreGuid;
        AccreditationInstanceGUID = accreditationInstanceGUID;
    }
}