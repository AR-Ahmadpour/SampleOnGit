using Accreditation.Application.Abstractions;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.Roles.GetAllRoleUser
{
    public sealed record GetAllRoleUserQuery(PagingParams PagingParams,string? FullName,string? NationalCode,int? RoleID, bool IsSetadi) : IQuery<PagedList<GetAllRoleUserDto>>;

}
