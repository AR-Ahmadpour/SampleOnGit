
using Accreditation.Domain.Sanjehs.Entities;
using Accreditation.Domain.ZirMehvars.Entities;
using SharedKernel;


namespace Accreditation.Domain.Standards.Entities;

public sealed class Standard : Entity
{
    public string Title { get; private set; }
    public string Code { get; private set; }
    public int SortOrder { get; private set; }
    public int WeightedCoefficient { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime? UpdateDate { get; private set; }
    public Guid CreatedByGUID { get; private set; }
    public Guid? UpdatedByGUID { get; private set; }
    public string ShortTitle { get; private set; }
    public Guid ZirMehvarGUID { get; private set; }
    public ZirMehvar ZirMehvar { get; private set; }
    //public ICollection<Sanjeh> Sanjehs { get; set; }    

    public Standard()
    {
        //Sanjehs = new List<Sanjeh>();   
    }

    private Standard(Guid id)
    {
        GUID = id;
    }

    private Standard(Guid zirMehvarGUID, string title, string shortTitle, string code, int sortOrder, int weightedCoefficient,
        DateTime creationDate, Guid createByGUID)
        : base(Guid.NewGuid())
    {
        ZirMehvarGUID = zirMehvarGUID;
        Title = title;
        ShortTitle = shortTitle;
        Code = code;
        IsDeleted = false;
        CreationDate = creationDate;
        UpdateDate = null;
        CreatedByGUID = createByGUID;
        UpdatedByGUID = null;
        SortOrder = sortOrder;
        WeightedCoefficient = weightedCoefficient;
    }

    public static Standard Create(Guid zirMehvarGUID, string title, string shortTitle, string code,
        int sortOrder, Guid createdByPersonGUID, DateTime creationDate, int weightCoefficient)
    {
        var standard = new Standard(zirMehvarGUID, title, shortTitle, code, sortOrder,
            weightCoefficient,creationDate, createdByPersonGUID);

        return standard;
    }

    public void Edit(string title, string shortTitle, string code, DateTime updateDate, Guid updateByGUID, 
        int sortOrder, int weightedCoefficient)
    {
        Title = title;
        ShortTitle = shortTitle;
        Code = code;
        UpdateDate = updateDate;
        UpdatedByGUID = updateByGUID;
        SortOrder = sortOrder;
        WeightedCoefficient = weightedCoefficient;
    }

    public void LogicalDelete(bool isDeleted) => IsDeleted = !isDeleted;

    public static Standard CreateById(Guid id) => new Standard(id);
}