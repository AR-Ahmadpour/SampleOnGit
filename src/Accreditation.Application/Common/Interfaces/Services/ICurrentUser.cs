using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Common.Interfaces.Services
{
    public interface ICurrentUser
    {
        string? UserId { get; }
        string? Roles { get; }
        //string? Permissions { get; }
        List<string>? Permissions { get; }
    }
}
