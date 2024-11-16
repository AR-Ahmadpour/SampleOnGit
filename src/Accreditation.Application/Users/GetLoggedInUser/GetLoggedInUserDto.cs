using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.GetLoggedInUser
{
    public sealed class GetLoggedInUserDto
    {
        public Guid Id { get; init; }
        public string NationalCode { get; init; }
        public string Email { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }
    }
}
