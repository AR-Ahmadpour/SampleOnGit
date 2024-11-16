using Accreditation.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.AddRoleUser
{
    public sealed record AddRoleUserCommand
    (
      int   RolesId,
      Guid  UsersGUID,
      Guid  CreateByGUID,
      int UniversityID
    ) :ICommand<bool>;
}
