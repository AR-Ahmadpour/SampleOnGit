namespace Accreditation.Api.Endpoints.EtebarDorehs.Adds;

public sealed record AddEtebarDorehRequest(
        Guid OrgTypeGUID,
        string Title,
        DateOnly StartDate,
        DateOnly EndDate,
        bool IsCurrent,
        int SortOrder);
