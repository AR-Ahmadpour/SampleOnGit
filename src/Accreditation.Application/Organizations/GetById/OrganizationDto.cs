namespace Accreditation.Application.Organizations.GetById;

public sealed record OrganizationDto
{
    public string? OrganizationTitle { get; init; }
    public string? OrgTypeTitle { get; init; }
    public string? University { get; init; }
    public Guid OrganizationGuid { get; init; }
    public Guid OrgTypeGuid { get; init; }
}
