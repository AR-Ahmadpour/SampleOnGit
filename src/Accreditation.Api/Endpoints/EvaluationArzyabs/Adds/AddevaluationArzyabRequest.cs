namespace Accreditation.Api.Endpoints.EvaluationArzyabs.Adds;
public sealed record AddevaluationArzyabRequest(
                                       int ArzyabRoleId,
                                       List<Guid> FieldIds,
                                       Guid EtebarDorehGuid,
                                       Guid AccreditationInstanceGuid,
                                       Guid ArzyabUserGuid);
