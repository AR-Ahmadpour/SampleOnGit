using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.EtebarDorehs.Add;

public sealed record AddEtebarDorehCommand
    (Guid OrgTypeGUID, string Title,
    DateOnly StartDate, DateOnly EndDate, bool IsCurrent, int SortOrder)
: ICommand<Guid>;
