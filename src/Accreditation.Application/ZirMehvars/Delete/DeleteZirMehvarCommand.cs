

using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.ZirMehvars.Delete;

public sealed record DeleteZirMehvarCommand(Guid GUID)
: ICommand;
