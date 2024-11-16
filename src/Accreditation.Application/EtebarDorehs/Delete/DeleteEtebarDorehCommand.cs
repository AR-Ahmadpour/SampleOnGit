using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.EtebarDorehs.Delete;
 
public sealed record DeleteEtebarDorehCommand(Guid GUID) 
    :ICommand;
