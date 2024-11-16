using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.Roles.GetRoleUserById
{
    public sealed record GetRoleUserByIdDto
    {
        public string? NationalCode { get; init; }
        public string? FullName { get; init; }
        public int RoleId { get; init; }
        public Guid UserId { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string UniversityTitel { get; init; }
        public int UniversityId { get; init; }

    }
}
