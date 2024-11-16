namespace Accreditation.Api.Endpoints.AccreditationInstances.Edits;
public sealed record EditMojadadRequest(
    DateOnly fromDate,
    DateOnly toDate,
    Guid? ArzyabSarparastGuid);