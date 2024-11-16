using Microsoft.AspNetCore.Identity;

namespace Accrediation.Domain.Identity;

public sealed class UserRole : IdentityUserRole<string>
{
    public User User { get; set; }
    public Role Role { get; set; }
}
