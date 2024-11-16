
using Accreditation.Application.Abstractions.Messaging;


namespace Accreditation.Application.Mehvars.Delete;



public sealed record DeleteMehvarCommand(Guid GUID)
    : ICommand;

