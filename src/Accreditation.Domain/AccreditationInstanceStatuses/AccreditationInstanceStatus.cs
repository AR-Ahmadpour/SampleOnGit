using SharedKernel;

namespace Accreditation.Domain.AccreditationInstanceStatuses;
public sealed class AccreditationInstanceStatus : Entity
{
    public int AccreditationInstanceStatusTypeId { get; private set; }
    public DateTime ChangeStatusDate { get; set; }
    public Guid AccreditationInstanceGUID { get; private set; }
    public Guid UserGUID { get; private set; }

    private AccreditationInstanceStatus()
    {

    }
    private AccreditationInstanceStatus(Guid accInstanceGuid,
                                        Guid userGuid,
                                        DateTime changeDateTime,
                                        int accStatusTyped) : base(Guid.NewGuid())
    {
        AccreditationInstanceGUID = accInstanceGuid;
        AccreditationInstanceStatusTypeId = accStatusTyped;
        ChangeStatusDate = changeDateTime;
        UserGUID = userGuid;
    }
    public static AccreditationInstanceStatus Create(Guid accInstanceGuid,
                                        Guid userGuid,
                                        DateTime changeDateTime,
                                        int accStatusTyped)
        => new AccreditationInstanceStatus(accInstanceGuid,
                                         userGuid,
                                         changeDateTime,
                                         accStatusTyped);
}
