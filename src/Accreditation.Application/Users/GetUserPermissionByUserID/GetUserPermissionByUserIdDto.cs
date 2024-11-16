using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.GetUserPermissionByUserID
{
    public sealed class GetUserPermissionByUserIdDto
    {
        public int    UserPermissionID { get; set; }
        public string PermissionTitel { get; set; }
        public string CategoryTitel { get; set; }
        public string CreateUserFullName { get; set; }
        public string UpdateFullName { get; set; }
        public bool Allow { get; set;}
    }

}
