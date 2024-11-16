using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.SanjehEnvironmentStandards.Add
{

    public sealed record AddSanjehEnvironmentStandardCommand(Guid SanjehGuid,
        List<Guid> SanjehEnvironmentStandardGuids)
         : ICommand;
}

