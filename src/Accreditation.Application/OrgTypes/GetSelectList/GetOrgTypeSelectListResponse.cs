namespace Accreditation.Application.OrgTypes.GetSelectList;

public sealed record GetOrgTypeSelectListResponse
{
    public Guid GUID { get; init; }
    public string Title { get; init; }
}
