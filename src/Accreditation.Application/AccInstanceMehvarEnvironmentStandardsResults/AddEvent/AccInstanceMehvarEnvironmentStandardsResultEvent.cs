using Accreditation.Application.Abstractions.EventBusService;
using MediatR;

namespace Accreditation.Application.AccInstanceMehvars.AddEvent;

public class AccInstanceMehvarEnvironmentStandardsResultEvent : IntegrationEvent, INotification
{
    public Guid AccreditationInstanceMehvarGUID { get; private set; }
    public Guid AccreditationInstanceGUID { get; private set; }
    public Guid EtebarDoreGuid { get; private set; }
    public List<Guid> environmentStandards { get; private set; }
    public AccInstanceMehvarEnvironmentStandardsResultEvent(Guid accreditationInstanceMehvarGUID,
                                                            Guid etebarDoreGuid,
                                                            Guid accreditationInstanceGUID
        , List<Guid>  EnvironmentStandards
        )
    {
        AccreditationInstanceMehvarGUID = accreditationInstanceMehvarGUID;
        EtebarDoreGuid = etebarDoreGuid;
        AccreditationInstanceGUID = accreditationInstanceGUID;
        environmentStandards = EnvironmentStandards;
    }
}