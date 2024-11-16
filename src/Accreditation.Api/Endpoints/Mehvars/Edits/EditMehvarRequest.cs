namespace Accreditation.Api.Endpoints.Mehvars.Edits;

public sealed record EditMehvarRequest(
    string Title,
    int WeightedCoefficient,
    int SortOrder);
