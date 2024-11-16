using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.GetAllPersons
{
    public sealed class GetAllUsersDto
    {
        public Guid UserGuid {get;  set;}
        public string? UserName { get;  set;} = string.Empty;
        public string FullName { get;  set;}
        public bool? IsDelete { get;  set; }

    }
}
