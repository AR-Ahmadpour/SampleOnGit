using Accreditation.Application.Abstractions.Messaging;


namespace Accreditation.Application.Standards.Delete
{
    public sealed record DeleteStandardCommand(Guid GUID)
    : ICommand;
}
