using Accreditation.Application.Abstractions.EventBusService;
using MediatR;

namespace Accreditation.Application.EvaluationArzyabs.AddEvent;

public class EvaluationArzyabEvent : IntegrationEvent, INotification
{
    public Guid AccreditationInstanceGUID { get; private set; }
    public Guid? ArzyabUserGUID { get; private set; }
    public Guid EtebarDoreGuid { get; private set; }

    public EvaluationArzyabEvent(Guid etebarDoreGuid, Guid accreditationInstanceGUID, Guid? arzyabUserGUID)
    {
        EtebarDoreGuid = etebarDoreGuid;
        AccreditationInstanceGUID = accreditationInstanceGUID;
        ArzyabUserGUID = arzyabUserGUID;
    }
}