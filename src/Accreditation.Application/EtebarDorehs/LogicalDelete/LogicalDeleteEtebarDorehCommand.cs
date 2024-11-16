using Accreditation.Application.Abstractions.Messaging; 

namespace Accreditation.Application.EtebarDorehs.LogicalDelete;

public sealed record LogicalDeleteEtebarDorehCommand(Guid GUID)
    :ICommand;
