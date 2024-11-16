using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.Roles.GetPermissionByUser
{
    public sealed class GetPermissionByUserDto
    {
        public int PermissionId { get; set; }
        public string PermissionTitle { get; set; }
        public string Desvrp { get; set; }
        public int RoleId {  get; set; }
        public bool PermissionIsDeleted {  get; set; }
        public string RoleTitle { get; set; }
        public bool Allowed {  get; set; }  
    }
}
