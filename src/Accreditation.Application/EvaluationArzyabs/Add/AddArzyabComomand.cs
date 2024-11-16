using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.EvaluationArzyabs.Add;
public sealed record AddArzyabComomand(int ArzyabRoleId,
                                       List<Guid> FieldIds,
                                       Guid EtebarDorehGuid,
                                       Guid AccreditationInstanceGuid,
                                       Guid ArzyabUserGuid) : ICommand<Guid>;
