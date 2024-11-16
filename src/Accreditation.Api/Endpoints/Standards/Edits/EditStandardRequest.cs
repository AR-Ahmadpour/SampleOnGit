namespace Accreditation.Api.Endpoints.Standards.Edits
{
    public sealed record EditStandardRequest(
       string Title,
       string ShortTitle,
       string Code,
       int WeightedCoefficient,
       int SortOrder);
}
