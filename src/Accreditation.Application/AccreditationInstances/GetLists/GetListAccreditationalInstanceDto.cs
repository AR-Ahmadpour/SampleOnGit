namespace Accreditation.Application.AccreditationInstances.GetList;

public class GetListAccreditationalInstanceDto
{
    public Guid Guid { get; set; }
    public Guid OrganizationGuid { get; set; }
    public Guid? AccreditationInstancePayehGuid { get; set; }
    public string OrganizationName { get; set; }
    public string InstanceTypeName { get; set; }
    public string? Status { get; set; }
    public string OrgTypeName { get; set; }
    public DateOnly? DateFrom { get; set; }
    public DateOnly? DateTo { get; set; }
    public Guid? SarparastGuid { get; set; }
    public string? SarparastName { get; set; }
    public decimal? PercentGrade { get; set; }
}