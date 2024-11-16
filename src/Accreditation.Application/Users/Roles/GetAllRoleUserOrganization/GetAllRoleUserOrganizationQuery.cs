using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Users.Roles.GetAllRoleUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.Roles.GetAllRoleUserOrganization
{ 
       public sealed record GetAllRoleUserOrganizationQuery(PagingParams PagingParams, string? FullName, string? NationalCode, int? RoleID) : IQuery<PagedList<GetAllRoleUserOrganizationDto>>;
}
