namespace Accreditation.Domain.Users;

public sealed class Role
{
    public Role()
    {
        RoleUsers = new List<RoleUser>();
        RolePermissions = new List<RolePermission>();
    }
    public Role(int id, string title,string description, bool isDeleted)
    {
        Id = id;
        Title = title;
        Description = description;
        IsDeleted = isDeleted;
    }

    public int Id { get; init; }

    public string Title { get; init; }

    public string Description { get; init; }

    public bool IsDeleted { get; init; }
    //public Guid? UserId { get; init; }
    /// <summary>
    /// نقش ستادی و ارزیاب کشوری
    /// </summary>
    public bool IsSetadi { get; init; }
    //public ICollection<User> Users { get; init; } = new List<User>();
    public ICollection<RoleUser> RoleUsers { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; }
}
