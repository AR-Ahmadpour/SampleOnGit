using Accrediation.Domain.Identity.Enums;
using Microsoft.AspNetCore.Identity;

namespace Accrediation.Domain.Identity;

public sealed class User : IdentityUser<string>
{
    public User()
    {
        Roles = new List<UserRole>();
    }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime RegisterDate { get; init; }
    public bool CompletelyRegistered { get; init; }
    public bool IsBlocked { get; init; }
    public Gender? Gender { get; init; }
    public string? Address { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? NationalCode { get; set; }
    public string? FatherName { get; set; }
    public ICollection<UserRole> Roles { get; set; }
}

public struct SystemRoles
{
    public const string NormalOperator = "NormalOperator";
    public const string MasterOperator = "MasterOperator";
    public const string Admin = "Admin";
}
