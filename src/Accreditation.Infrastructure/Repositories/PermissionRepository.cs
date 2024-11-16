using Accreditation.Application.Common.Interfaces.Persistence.Permissions;
using Accreditation.Application.Users.GetPermisionByCategory;
using Accreditation.Domain.Users;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Infrastructure.Repositories
{
    internal sealed class PermissionRepository(AccreditationDbContext _context): IPermissionRepository
    {
        public void Add(Permission permision)
        {
            _context.Permissions.Add(permision);
        }

      
    }
}
