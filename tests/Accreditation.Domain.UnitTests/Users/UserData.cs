using SharedKernel;

namespace Accreditation.Domain.UnitTests.Users;

internal static class UserData
{
    private static readonly DateTime currentDateTime =  DateTime.Now;
    private static readonly DateOnly currentDate = currentDateTime.ToDateOnly();

    public static readonly string FirstName  = "DUMMY_FirstName";
    public static readonly string LastName   = "DUMMY_LastName";
    public static readonly string FatherName = "DUMMY_FatherName";
    public static readonly string BirthCertNo = "DUMMY_BirthCertNo";
    public static readonly string BirthCertSerial = "DUMMY_BirthCertSerial";
    public static readonly string BirthPlace = "DUMMY_BirthPlace";
    public static readonly string Mobile = "09368525858";
    public static readonly string Tel = "8525436";
    public static readonly string Email = "test@test.com";
    public static readonly byte GenderId = 1;
    public static readonly DateOnly? DeathDate = currentDate;
    public static readonly bool IsAlive = true;
    public static readonly bool SabteAhvalApproved = true;
    public static readonly bool IsDeleted = false;
    public static readonly long? ImageId = 1;
    public static readonly string NationalCode = "2258521456";
    public static readonly DateOnly? BirthDate = currentDate;
    public static readonly bool AtbaKhareji = true;
    public static readonly Guid SSOUserId =  Guid.NewGuid() ;
    public static readonly DateTime CreationDate = currentDateTime;
    public static readonly DateTime? UpdateDate = currentDateTime.AddDays(2);
    public static readonly Guid CreatedByGUID = Guid.NewGuid();
    public static readonly Guid? UpdatedByGUID = Guid.NewGuid();
    public static readonly byte RoleId = 1;
    public static readonly string Password = "123";

}

