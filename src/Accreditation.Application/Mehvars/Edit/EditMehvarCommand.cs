using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Mehvars.Edit;

public sealed record EditMehvarCommand(Guid GUID,  
    string Title,int WeightedCoefficient, int SortOrder): ICommand<Guid>;
