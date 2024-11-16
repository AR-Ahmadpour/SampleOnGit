using Accreditation.Domain.EtebarDorehs.Entities;
using Accreditation.Domain.Tahsilats.Entities;
using Accreditation.Domain.UserDorehs.Entities;
using Accreditation.Domain.UserInfos.Entities;
using Accreditation.Domain.Users.Events;
using SharedKernel;

namespace Accreditation.Domain.Users;

public sealed class User : Entity
{
   
    private User(
        Guid id, string firstName, string lastName, string fatherName,
     string birthCertNo, string birthCertSerial, string birthPlace,
     string mobile, string tel, string email, byte genderId,
     DateOnly? deathDate, bool isAlive, bool sabteAhvalApproved,
     bool isDeleted, long? imageId, string nationalCode, DateOnly? birthDate,
     bool atbaKhareji,   DateTime creationDate,
     Guid createdByGUID,
     string password
        ) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        FatherName = fatherName;
        BirthCertNo = birthCertNo;
        BirthCertSerial = birthCertSerial;
        BirthPlace = birthPlace;
        Mobile = mobile;
        Tel = tel;
        Email = email;
        GenderId = genderId;
        DeathDate = deathDate;
        IsAlive = isAlive;
        SabteAhvalApproved = sabteAhvalApproved;
        IsDeleted = isDeleted;
        ImageId = imageId;
        NationalCode = nationalCode;
        BirthDate = birthDate;
        AtbaKhareji = atbaKhareji;
        CreatedDate = creationDate;
        UpdatedDate = null;
        CreatedByGUID = createdByGUID;
        UpdatedByGUID = null;
        Password = password;
    }

    public User()
    {
        Roles=new List<Role>();
        RoleUsers = new List<RoleUser>();
        UserPermissions = new List<UserPermission>();
    }

    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? FatherName { get; private set; }
    public string? BirthCertNo { get; private set; }
    public string? BirthCertSerial { get; private set; }
    public string? BirthPlace { get; private set; }
    public string? Mobile { get; private set; }
    public string? Tel { get; private set; }
    public string? Email { get; private set; }
    public byte? GenderId { get; private set; }
    public DateOnly? DeathDate { get; private set; }
    public bool? IsAlive { get; private set; }
    public bool? SabteAhvalApproved { get; private set; } = true;
    public bool? IsDeleted { get; private set; }
    public long? ImageId { get; private set; }
    public string? NationalCode { get; private set; }
    public DateOnly? BirthDate { get; private set; }
    public bool? AtbaKhareji { get; private set; }
    public Guid? SSOUserId { get; private set; }
    public DateTime? CreatedDate { get; private set; }
    public DateTime? UpdatedDate { get; private set; }
    public Guid? CreatedByGUID { get; private set; }
    public Guid? UpdatedByGUID { get; private set; }
    public string Password { get; private set; }
    public bool? MobileConfirmed { get; private set; }

    public ICollection<Role> Roles { get; set; }
    public ICollection<RoleUser> RoleUsers { get; set; }
    public ICollection<UserPermission> UserPermissions { get; set; }
    public UserInfo UserInfo{ get; set; }

     


    public static User Create(
     string firstName, string lastName, string fatherName,
     string birthCertNo, string birthCertSerial, string birthPlace,
     string mobile, string tel, string email, byte genderId,
     DateOnly? deathDate, bool isAlive, bool sabteAhvalApproved,
     bool isDeleted, long? imageId, string nationalCode, DateOnly? birthDate,
     bool atbaKhareji,  DateTime creationDate,
     Guid createdByGUID,byte roleId,
     string PassWord
        )
    {
        var user = new User(Guid.NewGuid(), firstName, lastName, fatherName,
        birthCertNo, birthCertSerial, birthPlace, mobile, tel, email, genderId,
        deathDate, isAlive, sabteAhvalApproved, isDeleted, imageId, nationalCode, birthDate,
        atbaKhareji, creationDate, createdByGUID, PassWord);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.GUID));

        //user._roles.Add(new Role(roleId, "title",   null,false));

        return user;
    }

    public void LogicalDelete(bool isDeleted) => IsDeleted = !isDeleted;

    public void SetSSOUserId(Guid identityId)
    {
        SSOUserId = identityId;
    }
}
