

using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.ZirMehvars.Add;

public sealed record AddZirMehvarCommand(Guid MehvarGuid, string Title, int WeightedCoefficient, int SortOrder) : ICommand<Guid>;
