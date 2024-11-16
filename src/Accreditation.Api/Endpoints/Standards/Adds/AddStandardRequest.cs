namespace Accreditation.Api.Endpoints.Standards.Adds;


public sealed record AddStandardRequest(
  Guid ZirMehvarGuid,
  string Title,
  string ShortTitle,
  string Code,
  int SortOrder,
  int WeightedCoefficient
  );
