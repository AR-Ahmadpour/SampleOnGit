namespace Accreditation.Domain.EtebarDorehs.Dtos;
public class GetListDto
{
    public Guid Guid { get; set; }
    public string Title { get; set; }
    public string OrgTypeTitle { get; set; }
    public DateTime CreationDate { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public Guid CreatedByGUID { get; set; }
    public string CreatedByFullUserName {  get; set; }
    public Guid? UpdatedByGUID { get; set; }
    public string UpdatedByFullUserName { get; set; }

    public int SortOrder { get; set; }
    public bool IsCurrent { get; init; }
    public bool  IsDeleted { get; init; }
}
