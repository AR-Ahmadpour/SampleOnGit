using Accreditation.Application.Abstractions.EventBusService;
using MediatR;

namespace Accreditation.Application.AccreditationalInstanceAnswers.AddEvent;

public class AccreditationalInstanceAnswerEvent : IntegrationEvent, INotification
{
    public Guid AccreditationInstanceGUID { get; private set; }
    public Guid EtebarDoreGuid { get; private set; }

    public AccreditationalInstanceAnswerEvent(Guid etebarDoreGuid,
                                              Guid accreditationInstanceGUID)
    {
        EtebarDoreGuid = etebarDoreGuid;
        AccreditationInstanceGUID = accreditationInstanceGUID;
    }
}