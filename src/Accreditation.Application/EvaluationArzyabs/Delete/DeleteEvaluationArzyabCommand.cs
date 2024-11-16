using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.EvaluationArzyabs.Delete;
 
public sealed record DeleteEvaluationArzyabCommand(Guid GUID) 
    :ICommand;
