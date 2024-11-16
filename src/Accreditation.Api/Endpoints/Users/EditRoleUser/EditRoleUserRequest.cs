namespace Accreditation.Api.Endpoints.Users.EditRoleUser
{
    public sealed record EditRoleUserRequest(int RoleUserId,int RolesID, Guid UserGuid, int UniversityID,bool IsActive);
}
