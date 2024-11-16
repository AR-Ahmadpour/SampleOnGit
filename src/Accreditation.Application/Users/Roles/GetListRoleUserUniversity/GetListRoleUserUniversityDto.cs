using Accreditation.Domain.Universites.Entities;
using Accreditation.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.Roles.GetListRoleUserUniversity
{
    public class GetListRoleUserUniversityDto
    {
        public int RoleId { get; init; }
        public string RoleTitle { get; init; }
        public int? UniversityId { get; init; }
        public string RoleDescription { get; init; }
        public Guid? OrganizationGUID { get; init; }
        public string? OrganizationTypeTitle { get; init; }
        public Guid? OrgTypeGUID { get; init; }

    }
}
