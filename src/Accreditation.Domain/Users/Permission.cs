using Accreditation.Domain.Categories.Entities;

namespace Accreditation.Domain.Users;

public sealed class Permission
{
    //public static readonly Permission UsersRead = new(1, "users:read");
 
    public Permission()
    {
        RolePermissions = new List<RolePermission>();
        UserPermissions = new List<UserPermission>();
    }
    private Permission(int id, string title, string Description, bool isDeleted,int CategoryID)
    {
        Id = id;
        Title = title;
        Description = Description;
        IsDeleted = isDeleted;
        CategoryId= CategoryID;
    }

    public int Id { get; init; }

    public string Title { get; init; }
    public string Description { get; init; }
    public bool IsDeleted { get; init; }
    public int CategoryId { get; init; }
    public Category category { get; init; }
    public ICollection<RolePermission> RolePermissions { get; init; }
    public ICollection<UserPermission> UserPermissions { get; init; }

    public static Permission Create(int id, string title, string Description, bool isDeleted, int CategoryID)
    {
        return new Permission( id,  title,  Description,  isDeleted,  CategoryID);
    }


}
