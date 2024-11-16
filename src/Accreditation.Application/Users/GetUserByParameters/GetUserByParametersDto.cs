using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.GetByParameters
{
    public sealed class GetUserByParametersDto
    {
       public  Guid   UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }   
        public bool?   IsAlive { get; set; }
        public bool?   IsDeleted { get; set; } = false;

    }

}
