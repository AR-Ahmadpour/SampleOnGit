﻿using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.Roles.GetListRoleUserUniversity
{
    public sealed record GetListRoleUserUniversityQuery (Guid UserId ,string DateNow)
        :IQuery<PagedList< GetListRoleUserUniversityDto>>
    {
    }
}