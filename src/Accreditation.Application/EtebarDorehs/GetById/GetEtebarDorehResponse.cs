namespace Accreditation.Application.EtebarDorehs.GetById;

public sealed record GetEtebarDorehResponse
{
    public Guid GUID { get; init; }
    public Guid OrgTypeGUID { get; init; }
    public string Title { get; init; }
    public DateOnly? StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
    public bool IsCurrent { get; init; }
    public bool IsDeleted { get; init; }
    public int SortOrder { get; init; }
}




