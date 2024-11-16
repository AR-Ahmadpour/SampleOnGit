using Accreditation.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.Roles.GetRoleUserDetail
{
    public sealed record GetRoleUserDetailQuery(int RoleId, Guid UserId) : IQuery<GetRoleUserDetailDto>;
}
