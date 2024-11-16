using Accreditation.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.Roles.GetRoleUserDetail
{
    public sealed record GetRoleUserDetailDto
    {

        public string NationalCode { get; init; }
        public string FullName { get; init; }
        public int Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public bool IsDeleted { get; init; }
        public bool IsSetadi { get; init; }

    }
}
