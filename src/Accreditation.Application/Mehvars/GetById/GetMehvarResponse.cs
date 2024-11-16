

namespace Accreditation.Application.Mehvars.GetById;

public sealed record GetMehvarResponse
{
    public string Title { get; init; }
    public string OrganizationTitle { get; init; }
    public string EtebarDoreTitle { get; init; }
    public int SortOrder { get; init; }
    public int? WeightedCoefficient { get; init; }
}
