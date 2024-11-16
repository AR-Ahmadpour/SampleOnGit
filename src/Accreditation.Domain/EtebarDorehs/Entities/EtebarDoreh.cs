using Accreditation.Domain.EtebarDorehs.Events;
using Accreditation.Domain.OrgTypes;
using Accreditation.Domain.Users;
using SharedKernel;

namespace Accreditation.Domain.EtebarDorehs.Entities;

public sealed class EtebarDoreh : Entity
{
    public Guid OrgTypeGUID { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public DateTime CreationDate { get; private set; }
    public DateTime? UpdateDate { get; private set; }
    public Guid CreatedByGUID { get; private set; }
    public Guid? UpdatedByGUID { get; private set; }
    public DateOnly? StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public bool IsCurrent { get; private set; }
    public bool IsDeleted { get; private set; }
    public int SortOrder { get; private set; }
    public OrgType OrgType { get; private set; } = null!;
    public User CreatedByUser { get; private set; } = null!;
    public User? UpdatedByUser { get; private set; }

    //public ICollection<Sanjeh> Sanjehs = null!;



    public EtebarDoreh()
    {
        //Sanjehs = new List<Sanjeh>();
    }

    private EtebarDoreh(Guid id) => GUID = id;

    private EtebarDoreh(Guid orgTypeGUID, string title,
        DateTime creationDate, Guid createdByGUID,
        DateOnly startDate, DateOnly endDate, bool isCurrent, int sortOrder)
        : base(Guid.NewGuid())
    {
        OrgTypeGUID = orgTypeGUID;
        Title = title;
        IsDeleted = false;
        CreationDate = creationDate;
        UpdateDate = null;
        CreatedByGUID =createdByGUID;
        UpdatedByGUID = null;
        StartDate = startDate;
        EndDate = endDate;
        IsCurrent = isCurrent;
        SortOrder = sortOrder;
    }

    public static EtebarDoreh Create(Guid orgTypeGUID, string title, 
        DateTime creationDate, Guid createdByGUID, DateOnly startDate,
        DateOnly endDate, bool isCurrent, int sortOrder)
    {
        var etebarDoreh = new EtebarDoreh (orgTypeGUID, title, creationDate, createdByGUID,
          startDate, endDate, isCurrent,  sortOrder);

         etebarDoreh.RaiseDomainEvent(new EtebarDorehDomainEvent(etebarDoreh.GUID));

        return etebarDoreh;
    }

    public void Edit(Guid orgTypeGUID, string title, DateTime updateDate, Guid updatedByGUID,
        DateOnly startDate, DateOnly endDate, bool isCurrent, int sortOrder)
    {
        OrgTypeGUID = orgTypeGUID;
        Title = title;
        UpdateDate = updateDate;
        UpdatedByGUID = updatedByGUID;
        StartDate = startDate;
        EndDate = endDate;
        IsCurrent = isCurrent;
        SortOrder = sortOrder;
    }

    public  void SetIsCurrentFalse() { IsCurrent = false; }
    public void LogicalDelete(bool isDeleted) => IsDeleted = !isDeleted;

    public static EtebarDoreh CreateById(Guid id) => new EtebarDoreh(id);
}
