using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accreditation.Application.Users.GetPermisionByCategory;
using Accreditation.Domain.Users;

namespace Accreditation.Application.Common.Interfaces.Persistence.Permissions
{
    public interface IPermissionRepository
    {
        void Add(Permission permision);
    }
}
