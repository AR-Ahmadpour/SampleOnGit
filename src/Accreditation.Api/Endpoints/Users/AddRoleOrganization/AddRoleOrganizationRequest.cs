namespace Accreditation.Api.Endpoints.Users.AddRoleOrganization
{
    public sealed record AddRoleOrganizationRequest(int RolesID, Guid UserGuid,Guid OrganizationID);

  
}
