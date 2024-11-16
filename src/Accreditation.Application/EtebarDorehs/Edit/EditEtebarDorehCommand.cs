using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.EtebarDorehs.Edit;

public sealed record EditEtebarDorehCommand
   (Guid GUID, 
    Guid OrgTypeGUID,
    string Title,
    DateOnly StartDate, 
    DateOnly EndDate,
    bool IsCurrent,
    int SortOrder) 
    : ICommand<Guid>;
