using Accreditation.Application.Common.Interfaces.Services;
using System.Security.Claims;


namespace Accreditation.Api.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?.User?.Claims
            .FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier)!.Value;


        public string? Roles => _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(_ => _.Type== ClaimTypes.Role)!.Value;

        //    public string? Permissions => _httpContextAccessor.HttpContext?.User.Claims
        //.FirstOrDefault(_ => _.Type == ClaimTypes.permissions)!.Value;

        public List<string>? Permissions
        {
            get
            {
                var permissionsClaim = _httpContextAccessor.HttpContext?.User.Claims
                    .FirstOrDefault(x => x.Type == "Permissions")?.Value;

                if (permissionsClaim != null)
                {
                    return permissionsClaim.Split(new char[] { ',' }).ToList();
                }

                return null;
            }
        }
    }
}
