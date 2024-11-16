using Accreditation.Application.Users.Roles.GetPermissionByUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Infrastructure.Services
{
    public class GetPermissionByUserDtoComparer : IEqualityComparer<GetPermissionByUserDto>
    {
        public bool Equals(GetPermissionByUserDto x, GetPermissionByUserDto y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return x.PermissionId == y.PermissionId && x.Allowed == y.Allowed;
        }

        public int GetHashCode(GetPermissionByUserDto obj)
        {
            int hashPermissionId = obj.PermissionId.GetHashCode();
            int hashAllowed = obj.Allowed.GetHashCode();
            return hashPermissionId ^ hashAllowed;
        }
    }
}
