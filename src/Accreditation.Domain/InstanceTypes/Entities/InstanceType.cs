using SharedKernel;

namespace Accreditation.Domain.InstanceType.Entities;

public sealed class InstanceType : Entity
{
    public int Id { get; set; }
    public string Title { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsActiveInUniversity { get; private set; }
    public bool IsActiveInStaff { get; private set; }

    public InstanceType()
    {
    }

    private InstanceType(Guid id) => GUID = id;

}
