namespace Accreditation.Api.Endpoints.EtebarDorehs.Edits;
public sealed record EditEtebarDorehRequest(
        Guid OrgTypeGUID,
        string Title,
        DateOnly StartDate,
        DateOnly EndDate,
        bool IsCurrent,
        int SortOrder);


