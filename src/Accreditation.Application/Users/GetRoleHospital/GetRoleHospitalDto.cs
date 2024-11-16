using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.GetRoleHospital
{
    public class GetRoleHospitalDto
    {
        public GetRoleHospitalDto(int roleId, string roleName, string roleDescription)
        {
            RoleId = roleId;
            RoleName = roleName;
            RoleDescription = roleDescription;
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }    = string.Empty;
        public string RoleDescription { get; set; } = string.Empty;
    }
}
