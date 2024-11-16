using Accreditation.Application.Abstractions.EventBusService;
using MediatR;

namespace Accreditation.Application.AccInstanceZirMehvars.AddEvent;

public class AccInstanceZirMehvarEnvironmentStandardsResultEvent : IntegrationEvent, INotification
{
    public Guid AccreditationInstanceZirMehvarGUID { get; private set; }
    public Guid AccreditationInstanceGUID { get; private set; }
    public Guid EtebarDoreGuid { get; private set; }

    public AccInstanceZirMehvarEnvironmentStandardsResultEvent(Guid accreditationInstanceZirMehvarGUID,
                                                            Guid etebarDoreGuid,
                                                            Guid accreditationInstanceGUID)
    {
        AccreditationInstanceZirMehvarGUID = accreditationInstanceZirMehvarGUID;
        EtebarDoreGuid = etebarDoreGuid;
        AccreditationInstanceGUID = accreditationInstanceGUID;
    }
}