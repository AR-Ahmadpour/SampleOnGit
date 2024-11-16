using Accreditation.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.GetUserPermissionByUserID
{
    public sealed record GetUserPermissionByUserIDQuery(Guid UserID):IQuery<List<GetUserPermissionByUserIdDto>>;
    
}
