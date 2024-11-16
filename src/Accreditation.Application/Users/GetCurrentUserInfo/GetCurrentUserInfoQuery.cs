using Accreditation.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.GetCurrentUserInfo
{
    public sealed record GetCurrentUserInfoQuery
        (
        Guid UserId,
        int RoleId,
        Guid OrganizationGuid,
        int UnivercityId
        ):IQuery<GetCurrentUserInfoQueryDto>;
}
