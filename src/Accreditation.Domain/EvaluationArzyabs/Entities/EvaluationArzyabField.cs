using Accreditation.Domain.Fields.Entities;
using SharedKernel;

namespace Accreditation.Domain.EvaluationArzyabs.Entities;

public sealed class EvaluationArzyabField : Entity
{
    public Guid EvaluationArzyabGUID { get; private set; }
    public Guid FieldGuid { get; private set; }
    public EvaluationArzyab EvaluationArzyab { get; private set; } = null!;
    public Field Field { get; private set; } = null!;

    private EvaluationArzyabField() { }


    private EvaluationArzyabField(Guid evaluationArzyabGuid,
                                  Guid fieldGuid) : base(Guid.NewGuid())
    {
        EvaluationArzyabGUID = evaluationArzyabGuid;
        FieldGuid = fieldGuid;
    }

    public static EvaluationArzyabField Create(Guid evaluationArzyabGuid,
                                               Guid fieldGuid)
    {
        return new EvaluationArzyabField(evaluationArzyabGuid, fieldGuid);
    }
}