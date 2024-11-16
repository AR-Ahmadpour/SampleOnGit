using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.AddPermission
{
    public sealed record  AddUserPermissionCommand(Guid UserGUID ,int PermissionId,Guid CreateByGUID,bool IsAllowed):ICommand;
}
