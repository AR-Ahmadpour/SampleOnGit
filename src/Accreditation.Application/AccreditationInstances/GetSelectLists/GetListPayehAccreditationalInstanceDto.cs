using Accreditation.Domain.InstanceType.Entities;

namespace Accreditation.Application.AccreditationInstances.GetSelectLists;

public class GetListPayehAccreditationalInstanceDto
{
    public Guid Guid { get; set; }
    public Guid OrganizationGuid { get; set; }
    public string OrganizationName { get; set; }
    public string EtebarDoreh { get; set; }
    public string InstanceTypeName { get; set; }
    public string? Status { get; set; }
    public string OrgTypeName { get; set; }
    public DateOnly? DateFrom { get; set; }
    public DateOnly? DateTo { get; set; }
    public Guid? SarparastGuid { get; set; }
    public string CreatedUserName { get; set; }
    public string? SarparastName { get; set; }
    public decimal? PercentGrade { get; set; }
}