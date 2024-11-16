namespace Accreditation.Application.ZirMehvars.Dtos;

public sealed class GetListByMehvarIdAsyncDto
{
    public Guid GUID { get; set; }
    public bool IsDeleted { get; set; }
    public string Title { get; set; }
    public int? WeightedCoefficient { get; set; }
    public int SortOrder { get; set; }
}
