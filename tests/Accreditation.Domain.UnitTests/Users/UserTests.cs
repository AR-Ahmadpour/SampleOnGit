using Accreditation.Domain.UnitTests.Infrastructure;
using Accreditation.Domain.Users;
using Accreditation.Domain.Users.Events;
using FluentAssertions;

namespace Accreditation.Domain.UnitTests.Users;

public class UserTests : BaseTest
{
    [Fact]
    public void Create_Should_SetPropertyValue()
    {
        // Act
        User user = CreateUser();

        // Assert
        user.FirstName.Should().Be(UserData.FirstName);
        user.LastName.Should().Be(UserData.LastName);
        user.FatherName.Should().Be(UserData.FatherName);
        user.BirthCertNo.Should().Be(UserData.BirthCertNo);
        user.BirthCertSerial.Should().Be(UserData.BirthCertSerial);
        user.BirthPlace.Should().Be(UserData.BirthPlace);
        user.Mobile.Should().Be(UserData.Mobile);
        user.Tel.Should().Be(UserData.Tel);
        user.Email.Should().Be(UserData.Email);
        user.GenderId.Should().Be(UserData.GenderId);
        user.DeathDate.Should().Be(UserData.DeathDate);
        user.IsAlive.Should().Be(UserData.IsAlive);
        user.SabteAhvalApproved.Should().Be(UserData.SabteAhvalApproved);
        user.IsDeleted.Should().Be(UserData.IsDeleted);
        user.ImageId.Should().Be(UserData.ImageId);
        user.NationalCode.Should().Be(UserData.NationalCode);
        user.BirthDate.Should().Be(UserData.BirthDate);
        user.AtbaKhareji.Should().Be(UserData.AtbaKhareji);
        user.CreatedDate.Should().Be(UserData.CreationDate);
        user.CreatedByGUID.Should().Be(UserData.CreatedByGUID);
        user.UpdatedDate.Should().BeNull();
        user.UpdatedByGUID.Should().BeNull();
    }

    [Fact]
    public void Create_Should_RaiseUserCreatedDomainEvent()
    {
        // Act
        User user = CreateUser();

        // Assert
        UserCreatedDomainEvent userCreatedDomainEvent = AssertDomainEventWasPublished<UserCreatedDomainEvent>(user);

        userCreatedDomainEvent.UserGUID.Should().Be(user.GUID);
    }

    //[Fact]
    //public void Create_Should_AddRegisteredRoleToUser()
    //{
    //    // Act
    //    User user = CreateUser();

    //    // Assert
    //    var role = user.Roles.SingleOrDefault();
    //    role.Id.Should().Be(UserData.RoleId);
    //    role.Title.Should().Be("title");
    //    role.IsDeleted.Should().Be(false);
    //    role.Description.Should().Be(null);
    //}

    private static User CreateUser()
    {
        return User.Create(
         UserData.FirstName, UserData.LastName, UserData.FatherName,
         UserData.BirthCertNo, UserData.BirthCertSerial, UserData.BirthPlace,
         UserData.Mobile, UserData.Tel, UserData.Email, UserData.GenderId,
         UserData.DeathDate, UserData.IsAlive, UserData.SabteAhvalApproved,
         UserData.IsDeleted, UserData.ImageId, UserData.NationalCode, UserData.BirthDate,
         UserData.AtbaKhareji, UserData.CreationDate,
         UserData.CreatedByGUID, UserData.RoleId, UserData.Password);
    }
}