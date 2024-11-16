using Accreditation.Application.Abstractions.Messaging;


namespace Accreditation.Application.Mehvars.Add;

public sealed record AddMehvarCommand(Guid EtebarDorehGUID,
    string Title, int WeightedCoefficient, int SortOrder): ICommand<Guid>;
