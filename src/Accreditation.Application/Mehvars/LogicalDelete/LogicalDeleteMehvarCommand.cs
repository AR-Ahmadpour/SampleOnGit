using Accreditation.Application.Abstractions.Messaging; 

namespace Accreditation.Application.EtebarDorehs.LogicalDelete;

public sealed record LogicalDeleteMehvarCommand(Guid GUID)
    :ICommand;
