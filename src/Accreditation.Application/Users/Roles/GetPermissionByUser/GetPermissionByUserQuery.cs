using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Users.GetAccessToken;
using Accreditation.Application.Users.Roles.GetRoleUserById;
using Accreditation.Domain.Universites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.Roles.GetSelectedRole
{
    public sealed record GetPermissionByUserQuery(int RoleId,  Guid UserId ,int? UniversityId, Guid? OrganizationGUID ,string? OrganizationTypeTitle, Guid? OrgTypeGUID) : IQuery<AccessTokenResponse>;
    //GetPermissionByUserDto
}
