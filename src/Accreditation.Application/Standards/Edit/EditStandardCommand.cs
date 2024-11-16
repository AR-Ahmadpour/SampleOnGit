using Accreditation.Application.Abstractions.Messaging;


namespace Accreditation.Application.Standards.Edit;

public sealed record EditStandardCommand(
    Guid GUID, string Title,string ShortTitle,string Code,
    int WeightedCoefficient, int SortOrder)
    : ICommand<Guid>;

