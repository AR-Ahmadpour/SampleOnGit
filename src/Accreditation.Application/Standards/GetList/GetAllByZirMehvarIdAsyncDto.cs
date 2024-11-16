namespace Accreditation.Domain.Standards.Dtos;

public sealed class GetAllByZirMehvarIdAsyncDto
{
    public Guid GUID { get; set; }
    public string Code { get; set; }
    public string? Title { get; set; }
    public int? WeightedCoefficient { get; set; }
    public int SortOrder { get; set; }
    public bool IsDeleted { get; set; }
}
