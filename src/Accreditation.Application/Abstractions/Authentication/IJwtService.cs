using Accreditation.Application.Users.Roles.GetSelectedRole;
using SharedKernel;
using System.Security.Claims;

namespace Accreditation.Application.Abstractions.Authentication;

public interface IJwtService
{
    Task<Result<string>> GetAccessTokenAsync(
        //string username,
        //string password,
        GetPermissionByUserQuery UserId
        ,CancellationToken cancellationToken = default);
}
