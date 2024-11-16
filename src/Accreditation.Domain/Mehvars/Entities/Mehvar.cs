using Accreditation.Domain.EtebarDorehs.Entities;
using Accreditation.Domain.ZirMehvars.Entities;
using SharedKernel;


namespace Accreditation.Domain.Mehvars.Entities;

public sealed class Mehvar : Entity
{
    public string Title { get; private set; } 
    public int SortOrder { get; private set; }
    public int? WeightedCoefficient { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime? UpdateDate { get; private set; }
    public Guid CreatedByGUID { get; private set; }
    public Guid? UpdatedByGUID { get; private set; }
    public Guid EtebarDorehGUID { get; private set; }
    public EtebarDoreh EtebarDoreh { get; private set; }

    public Mehvar()
    {
        //sanjehs = new List<Sanjeh>();
    }

    private Mehvar(Guid id) => GUID = id;

    private Mehvar(string title, int sortOrder, int weightedCoefficient,
        DateTime creationDate, Guid createdByGUID, Guid etebarDorehGUID)
        : base(Guid.NewGuid())
    {
        EtebarDorehGUID = etebarDorehGUID;
        Title = title;
        IsDeleted = false;
        CreationDate = creationDate;
        UpdateDate = null;
        CreatedByGUID = createdByGUID;
        UpdatedByGUID = null;
        SortOrder = sortOrder;
        WeightedCoefficient = weightedCoefficient;
    }

    public static Mehvar Create(Guid etebarDorehGUID, string title, int sortOrder,
        Guid createByUserGUID, DateTime creationDate, int weightedCoefficient)
    {
        return new Mehvar(title, sortOrder, weightedCoefficient, creationDate,
         createByUserGUID, etebarDorehGUID);
    }

    public void Edit(string title, DateTime updateDate, Guid updatedByGUID,
        int sortOrder, int weightedCoefficient)
    {
        Title = title;
        UpdateDate = updateDate;
        UpdatedByGUID = updatedByGUID;
        SortOrder = sortOrder;
        WeightedCoefficient = weightedCoefficient;
    }

    public void LogicalDelete(bool isDeleted) => IsDeleted = !isDeleted;

    public static Mehvar CreateById(Guid id) => new Mehvar(id);

}
