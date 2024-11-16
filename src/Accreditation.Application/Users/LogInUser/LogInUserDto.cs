using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.LogInUser
{
   public class LogInUserDto
    {
        public Guid    UserGuid { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public string TempToken { get; set; }
    }
}
