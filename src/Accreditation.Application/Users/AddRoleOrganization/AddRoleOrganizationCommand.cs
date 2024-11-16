using Accreditation.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.AddRoleOrganization
{
    public sealed record AddRoleOrganizationCommand(
      int RolesId,
      Guid UsersGUID,
      Guid CreateByGUID,
      Guid OrganizationID
    ) : ICommand<bool>;
}
