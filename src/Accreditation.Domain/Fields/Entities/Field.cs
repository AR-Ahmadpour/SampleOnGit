using Accreditation.Domain.EtebarDorehs.Entities;
using Accreditation.Domain.EvaluationArzyabs.Entities;
using SharedKernel;

namespace Accreditation.Domain.Fields.Entities;

public sealed class Field : Entity
{
    public Guid EtebarDorehGUID { get; private set; }
    public string Title { get; private set; }
    public string TitleCode { get; private set; }
    public string InstanceTypeIds { get; private set; }
    public bool IsDeleted { get; private set; }

    public EtebarDoreh EtebarDoreh { get; private set; } = null!;
    public ICollection<EvaluationArzyabField> EvaluationArzyabFields { get; private set; } = new List<EvaluationArzyabField>();

    private Field()
    {

    }

    private Field(Guid etebarDorehGuid,
                  string titile,
                  string titleCode,
                  string instanceTypeIds)
                  : base(Guid.NewGuid())
    {
        EtebarDorehGUID = etebarDorehGuid;
        Title = titile;
        TitleCode = titleCode;
        InstanceTypeIds = instanceTypeIds;
        IsDeleted = false;
    }

    public static Field Create(Guid etebarDorehGuid,
                               string titile,
                               string titleCode,
                               string instanceTypeIds)
    {
        return new Field(etebarDorehGuid, titile, titleCode, instanceTypeIds);
    }

    public void Edit(string title, string titleCode, string instanceTypeIds)
    {
        Title = title;
        TitleCode = titleCode;
        InstanceTypeIds = instanceTypeIds;
    }


}