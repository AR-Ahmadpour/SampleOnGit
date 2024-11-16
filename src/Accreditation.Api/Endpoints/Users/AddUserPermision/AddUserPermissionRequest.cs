namespace Accreditation.Api.Endpoints.Users.AddUserPermision
{
 
       public sealed record AddUserPermissionRequest(Guid UserGUID, int PermissionId, Guid CreateByGUID, bool IsAllowed) ;

}
