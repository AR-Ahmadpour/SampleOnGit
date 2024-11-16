using Accreditation.Domain.AccInstanceStandards.Entities;
using Accreditation.Domain.AccreditationInstanceStatusTypes.Entities;
using Accreditation.Domain.EtebarDorehs.Entities;
using Accreditation.Domain.EvaluationArzyabs.Entities;
using Accreditation.Domain.Organizations.Entities;
using Accreditation.Domain.Users;
using SharedKernel;

namespace Accreditation.Domain.AccreditationInstances.Entities;

public sealed class AccreditationInstance : Entity
{
    public int AccreditationInstanceStatusTypeId { get; private set; }
    public int InstanceTypeId { get; private set; }
    public Guid? MasterGUID { get; private set; }
    public Guid EtebarDorehGUID { get; private set; }
    public Guid CreateByUserGUID { get; private set; }
    public bool IsFinal { get; private set; }
    public bool IsLast { get; private set; }
    public DateTime? SubmitDate { get; private set; }
    public decimal LevelOneScore { get; private set; }
    public int LevelOnePossibleScore { get; private set; }
    public int LevelThreePossibleScore { get; private set; }
    public decimal LevelTwoScore { get; private set; }
    public decimal LevelThreeScore { get; private set; }
    public DateTime? CreateDateTime { get; private set; }
    public DateTime? UpdateDateTime { get; private set; }
    public Guid? UpdateByUserGUID { get; private set; }
    public Guid OrganizationGUID { get; private set; }
    public DateOnly? FromDate { get; set; }
    public DateOnly? ToDate { get; set; }
    public bool IsDeleted { get; set; }
    public int CalculationVersionId { get; set; }
    public bool IsLocked { get; set; }
    /// <summary>
    /// بصورت دستی با تریگر در دیتابیس اضافه میگردد
    /// </summary>
    
    public AccreditationInstance? MasterAccreditationInstance { get; private set; } = null!;
    public EtebarDoreh EtebarDoreh { get; private set; } = null!;
    public ICollection<EvaluationArzyab>? EvaluationArzyabs { get; set; }
    //public ICollection<AccInstanceStandard> AccInstanceStandards { get; private set; } =null!;
    public User CreatedUser { get; private set; } = null!;
    public AccreditationInstanceStatusType AccreditationInstanceStatusType { get; private set; } = null!;
    public Accreditation.Domain.InstanceType.Entities.InstanceType InstanceType { get; private set; } = null!;
    public Organization Organization { get; private set; } = null!;

    private AccreditationInstance(Guid id) => GUID = id;
    private AccreditationInstance()
    {
        //EvaluationArzyabs = new List<EvaluationArzyab>();
        //AccInstanceStandards = new List<AccInstanceStandard>();
    }
    private AccreditationInstance(Guid etebarDorehGUID,
                              Guid organizationGuid,
                              int instanceTypeId,
                              DateOnly? fromDate,
                              DateOnly? toDate,
                              Guid userGuid,
                              DateTime dateTime,
                              Guid? masterGuid,
                              int statusTypeId,
                              bool isLocked)
        : base(Guid.NewGuid())
    {
        EtebarDorehGUID = etebarDorehGUID;
        OrganizationGUID = organizationGuid;
        InstanceTypeId = instanceTypeId;
        FromDate = fromDate;
        ToDate = toDate;
        CreateByUserGUID = userGuid;
        IsDeleted = false;
        CreateDateTime = dateTime;
        IsLocked = isLocked;
        AccreditationInstanceStatusTypeId = statusTypeId;
        MasterGUID = masterGuid == null ? null : masterGuid.Value;
        // CalculationVersionId بصورت دستی با تریگر در دیتابیس اضافه میگردد
    }
    public static AccreditationInstance Create(Guid etebarDorehGUID,
                                            Guid organizationGuid,
                                            int instanceTypeId,
                                            DateOnly? fromDate,
                                            DateOnly? toDate,
                                            DateTime dateTime,
                                            Guid userGuid,
                                            Guid? masterGuid,
                                            int statusTypeId,
                                            bool isLocked) =>
        new AccreditationInstance(etebarDorehGUID,
                                organizationGuid,
                                instanceTypeId,
                                fromDate,
                                toDate,
                                userGuid,
                                dateTime,
                                masterGuid,
                                statusTypeId,
                                isLocked);

    public void Edit(DateOnly? fromDate,
                     DateOnly? toDate,
                     Guid updateUserGuid,
                     DateTime updateDateTime)
    {
        UpdateByUserGUID = updateUserGuid;
        FromDate = fromDate;
        ToDate = toDate;
        UpdateDateTime = updateDateTime;
    }
}
