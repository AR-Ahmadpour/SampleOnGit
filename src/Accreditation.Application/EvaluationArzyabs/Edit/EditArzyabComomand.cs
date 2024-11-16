using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.EvaluationArzyabs.Edit;
public sealed record EditArzyabComomand(int ArzyabRoleId,
                                       List<Guid> FieldIds,
                                       Guid EvaluationArzyabGuid) : ICommand<Guid>;
