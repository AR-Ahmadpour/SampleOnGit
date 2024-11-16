

using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.ZirMehvars.Edit
{
    public sealed record EditZirMehvarCommand
        (Guid GUID, string Title,
        int WeightedCoefficient, int SortOrder)
        : ICommand<Guid>;
}
