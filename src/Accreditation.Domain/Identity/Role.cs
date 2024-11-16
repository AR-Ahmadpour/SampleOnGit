using Microsoft.AspNetCore.Identity;

namespace Accrediation.Domain.Identity;

public sealed class Role : IdentityRole<string>
{
    public Role()
    {
        Users = new List<UserRole>();
    }

    public ICollection<UserRole> Users { get; set; }
}
