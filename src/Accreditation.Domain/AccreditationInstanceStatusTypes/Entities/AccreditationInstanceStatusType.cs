using SharedKernel;

namespace Accreditation.Domain.AccreditationInstanceStatusTypes.Entities;
public sealed class AccreditationInstanceStatusType : Entity
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Code { get; private set; }
    public bool IsLocked { get; private set; }
    public bool IsFinalStatus { get; private set; }
    public bool IsDestinyStatus { get; private set; }
    public int InstanceTypeId { get; private set; }
    public int StepOrder { get; private set; }
}
