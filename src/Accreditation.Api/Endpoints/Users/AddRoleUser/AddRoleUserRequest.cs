using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Accreditation.Api.Endpoints.Users.AddRoleUser
{
    public sealed record AddRoleUserRequest(int RolesID, Guid UserGuid,int UniversityID);

}
