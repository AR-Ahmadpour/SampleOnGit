using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.AccreditationInstances.Delete;
public sealed record DeleteAccInstanceCommand(Guid GUID) : ICommand;
