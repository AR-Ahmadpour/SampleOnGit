namespace Accreditation.Api.Endpoints.NotNaSanjeh.Adds
{
    public sealed record AddNotNaSanjehRequest(
    Guid SanjehGuid,
    List<Guid> OrgGerayeshGuids);
}
