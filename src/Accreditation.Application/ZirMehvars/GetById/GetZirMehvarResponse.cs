

namespace Accreditation.Application.ZirMehvars.GetById;

public sealed record GetZirMehvarResponse
{
    public string Title { get; init; }
    public int SortOrder { get; init; }
    public int? WeightedCoefficient { get; init; }

    public Guid Guid { get; init; }
}
