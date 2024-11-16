using Accreditation.Domain.AccreditationInstances.Entities;
using Accreditation.Domain.Users;
using SharedKernel;

namespace Accreditation.Domain.EvaluationArzyabs.Entities;

public sealed class EvaluationArzyab : Entity
{
    public Guid CreateUserGUID { get; private set; }
    public Guid AccreditationInstanceGUID { get; private set; }
    public Guid ArzyabUserGUID { get; private set; }
    public int ArzyabRoleId { get; private set; }
    public DateTime CreateDate { get; private set; }

    public AccreditationInstance AccreditationInstance { get; set; }
    public User User { get; private set; } = null!;
    public ICollection<EvaluationArzyabField> EvaluationArzyabFields { get; private set; } = null!;

    private EvaluationArzyab()
    {
        EvaluationArzyabFields = new List<EvaluationArzyabField>();
    }

    private EvaluationArzyab(Guid createUserGuid,
                             Guid accInstanceGuid,
                             Guid arzyabUserGuid,
                             int arzyabRoleId,
                             DateTime createDateTime)
                           : base(Guid.NewGuid())
    {
        CreateDate = createDateTime;
        CreateUserGUID = createUserGuid;
        ArzyabUserGUID = arzyabUserGuid;
        ArzyabRoleId = arzyabRoleId;
        AccreditationInstanceGUID = accInstanceGuid;
    }

    public static EvaluationArzyab Create(Guid createUserGuid,
                                          Guid accInstanceGuid,
                                          Guid arzyabUserGuid,
                                          int arzyabRoleId,
                                          DateTime createDateTime)
    {
        return new EvaluationArzyab(createUserGuid, accInstanceGuid, arzyabUserGuid, arzyabRoleId, createDateTime);
    }

    public void Edit(int arzyabRoleId)
    {
        ArzyabRoleId = arzyabRoleId;
    }
}
