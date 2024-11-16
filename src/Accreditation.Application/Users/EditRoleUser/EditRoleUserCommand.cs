using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.EditRoleUser
{
    public sealed record EditRoleUserCommand
    (
      int RoleUserId,
      int RoleId,
      Guid UsersGUID,
      Guid UpdateByGUID,
     int UniversityID,
      bool IsActive
    ) : ICommand<int>;
}
