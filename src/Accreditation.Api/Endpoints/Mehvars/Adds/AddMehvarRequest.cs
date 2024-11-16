namespace Accreditation.Api.Endpoints.Mehvars.Adds
{
    public sealed record AddMehvarRequest(
          Guid EtebarDorehGuid,
          string Title,
          int WeightedCoefficient,
          int SortOrder);
}
