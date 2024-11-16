using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.GetAllPersons
{
    public sealed record GetAllUsersQuery
    (
         string? NationalCode,
         string? Name,
        PagingParams PagingParams) :IQuery<PagedList<GetAllUsersDto>>;
}
