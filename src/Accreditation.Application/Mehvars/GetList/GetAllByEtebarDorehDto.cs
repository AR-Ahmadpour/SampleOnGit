namespace Accreditation.Domain.Mehvars.Dtos;

public sealed class GetAllByEtebarDorehDto
{
    public Guid Guid { get; set; }
    public string? Title { get; set; }
    public string EtebarDorehTitle { get; set; }
    public string OrgTypeTitle { get; set; }
    public int? WeightedCoefficient { get; set; }
    public int SortOrder { get; set; }
    public bool IsDeleted { get; set; }
}
