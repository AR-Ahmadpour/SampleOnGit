namespace Accreditation.Api.Endpoints.ZirMehvars.Adds
{
    public sealed record AddZirMehvarRequest(
       Guid MehvarGuid,
       string Title,
       int WeightedCoefficient,
       int SortOrder);
}
