namespace Accreditation.Api.Endpoints.Users.EditRoleOrganization
{
   public sealed record EditRoleOrganizationRequest(int RoleUserId, int RolesID, Guid UserGuid, Guid OrganizationID, bool IsActive);

}
