using Accreditation.Application.Abstractions.Authentication;
using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Application.ZirMehvars;
using Accreditation.Application.ZirMehvars.Edit;
using Accreditation.Domain.ZirMehvars.Entities;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace Accreditation.Application.UnitTests.ZirMehvars;

public class EditZirMehvarCommandTests
{
    private static readonly DateTime Now = DateTime.Now;
    private static readonly EditZirMehvarCommand Command = new(Guid.NewGuid(), "DUMMY_Tilte", 85, 30);

    private readonly EditZirMehvarCommandHandler handler;
    private readonly IZirMehvarRepository ZirMehvarRepositoryMock;
    private readonly ICurrentUser userContextMock;
    private readonly IDateTimeProvider dateTimeProviderMock;
    private readonly IUnitOfWork unitOfWorkMock;

    public EditZirMehvarCommandTests()
    {
        ZirMehvarRepositoryMock = Substitute.For<IZirMehvarRepository>();
        userContextMock = Substitute.For<ICurrentUser>();
        dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        dateTimeProviderMock.Now.Returns(Now);
        unitOfWorkMock = Substitute.For<IUnitOfWork>();

        handler = new EditZirMehvarCommandHandler(ZirMehvarRepositoryMock, userContextMock, dateTimeProviderMock, unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenZirMehvarNotExist()
    {
        //Arrange
        ZirMehvarRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns((ZirMehvar?)null);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(ZirMehvarErrors.NotFound);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenTitleIsNotUnique()
    {
        //Arrange
        var zirMehvar = ZirMehvarData.Create();
        ZirMehvarRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(zirMehvar);
        ZirMehvarRepositoryMock.IsTitleUniqueAsync(Command.GUID, zirMehvar.MehvarGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(false);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(ZirMehvarErrors.TitleNotUnique);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenEditSucceeds()
    {
        //Arrange
        var zirMehvar = ZirMehvarData.Create();
        var mehvarGUID = zirMehvar.MehvarGUID;
        var creationDate = zirMehvar.CreationDate;
        var createdByGUID = zirMehvar.CreatedByGUID;
        var isDeleted = zirMehvar.IsDeleted;
        ZirMehvarRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(zirMehvar);
        ZirMehvarRepositoryMock.IsTitleUniqueAsync(Command.GUID, zirMehvar.MehvarGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);
        var currentUserGUId = Guid.NewGuid().ToString();
        userContextMock.UserId.Returns(currentUserGUId);
        //Act
        Result<Guid> result = await handler.Handle(Command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(zirMehvar.GUID);
        result.Value.Should().Be(zirMehvar.GUID);
        zirMehvar.Title.Should().Be(Command.Title);
        zirMehvar.UpdatedByGUID.Should().NotBeNull();
        zirMehvar.UpdatedByGUID.Should().Be(currentUserGUId);
        zirMehvar.UpdateDate.Should().NotBeNull();
        zirMehvar.WeightedCoefficient.Should().Be(Command.WeightedCoefficient);
        zirMehvar.SortOrder.Should().Be(Command.SortOrder);

        zirMehvar.CreationDate.Should().Be(creationDate);
        zirMehvar.CreatedByGUID.Should().Be(createdByGUID);
        zirMehvar.IsDeleted.Should().Be(isDeleted);
        zirMehvar.MehvarGUID.Should().Be(mehvarGUID);
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenEditSucceeds()
    {
        //Arrange
        var zirMehvar = ZirMehvarData.Create();
        ZirMehvarRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(zirMehvar);
        ZirMehvarRepositoryMock.IsTitleUniqueAsync(Command.GUID, zirMehvar.MehvarGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result<Guid> result = await handler.Handle(Command, default);

        //Assert
        await unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }
}

