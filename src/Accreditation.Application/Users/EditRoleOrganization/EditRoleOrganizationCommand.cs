using Accreditation.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.EditRoleOrganization
{
    public sealed record EditRoleOrganizationCommand
    (
      int RoleUserId,
      int RoleId,
      Guid UsersGUID,
      Guid UpdateByGUID,
      Guid OrganizationID,
      bool IsActive
    ) : ICommand<int>;
}
