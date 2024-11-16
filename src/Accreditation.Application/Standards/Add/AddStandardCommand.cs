using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Standards.Add
{
    public sealed record AddStandardCommand
    (Guid ZirMehvarGUID, string Title, string ShortTitle,  string Code, int WeightedCoefficient,  int SortOrder)
    : ICommand<Guid>;
}
