using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Users.Roles.GetAllRoleUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.Roles.GetRoleUserById
{
    public sealed record GetRoleUserByIdQuery(PagingParams PagingParams,Guid UserID):IQuery<PagedList<GetRoleUserByIdDto>>;
}
