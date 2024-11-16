

using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.ZirMehvars.LogicalDelete
{
    public sealed record LogicalDeleteZirMehvarCommand(Guid GUID)
     : ICommand;
}
