

using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Standards.LogicalDelete
{
    public sealed record LogicalDeleteStandardCommand(Guid GUID)
    : ICommand;
}
