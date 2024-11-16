using Accreditation.Application.Abstractions.Authentication;
using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.Standards;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Application.Standards;
using Accreditation.Application.Standards.Edit;
using Accreditation.Domain.Standards.Entities;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace Accreditation.Application.UnitTests.Standards;

public class EditStandardCommandTests
{
    private static readonly DateTime Now = DateTime.Now;
    private static readonly Guid Id = Guid.NewGuid();
    private static readonly EditStandardCommand Command = new(Id, "DUMMY_Tilte", "DUMMY_ShortTilte", "34534", 65, 54);

    private readonly EditStandardCommandHandler handler;
    private readonly IStandardRepository StandardRepositoryMock;
    private readonly ICurrentUser userContextMock;
    private readonly IDateTimeProvider dateTimeProviderMock;
    private readonly IUnitOfWork unitOfWorkMock;

    public EditStandardCommandTests()
    {
        StandardRepositoryMock = Substitute.For<IStandardRepository>();
        userContextMock = Substitute.For<ICurrentUser>();
        dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        dateTimeProviderMock.Now.Returns(Now);
        unitOfWorkMock = Substitute.For<IUnitOfWork>();

        handler = new EditStandardCommandHandler(StandardRepositoryMock, userContextMock, dateTimeProviderMock, unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenStandardNotExist()
    {
        //Arrange
        StandardRepositoryMock.FindAsync(Id, Arg.Any<CancellationToken>()).Returns((Standard?)null);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(StandardErrors.NotFound);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenTitleIsNotUnique()
    {
        //Arrange
        var standard = StandardData.Create();
        StandardRepositoryMock.FindAsync(Id, Arg.Any<CancellationToken>()).Returns(standard);
        StandardRepositoryMock.IsTitleUniqueAsync(Id, standard.ZirMehvarGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(false);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(StandardErrors.TitleNotUnique);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenCodeIsNotUnique()
    {
        //Arrange
        var standard = StandardData.Create();
        StandardRepositoryMock.FindAsync(Id, Arg.Any<CancellationToken>()).Returns(standard);
        StandardRepositoryMock.IsTitleUniqueAsync(Id, standard.ZirMehvarGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);
        StandardRepositoryMock.IsCodeUnique(Id, standard.ZirMehvarGUID, Command.Code, Arg.Any<CancellationToken>()).Returns(false);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(StandardErrors.CodeNotUnique);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenEditSucceeds()
    {
        //Arrange
        var standard = StandardData.Create();
        var zirMehvarGUID = standard.ZirMehvarGUID;
        var creationDate = standard.CreationDate;
        var createdByGUID = standard.CreatedByGUID;
        var isDeleted = standard.IsDeleted;
        StandardRepositoryMock.FindAsync(Id, Arg.Any<CancellationToken>()).Returns(standard);
        StandardRepositoryMock.IsTitleUniqueAsync(Id, standard.ZirMehvarGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);
        StandardRepositoryMock.IsCodeUnique(Id, standard.ZirMehvarGUID, Command.Code, Arg.Any<CancellationToken>()).Returns(true);
        var currentUserGUId = Guid.NewGuid().ToString();
        userContextMock.UserId.Returns(currentUserGUId);

        //Act
        Result<Guid> result = await handler.Handle(Command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(standard.GUID);
        standard.Title.Should().Be(Command.Title);
        standard.ShortTitle.Should().Be(Command.ShortTitle);
        standard.Code.Should().Be(Command.Code);
        standard.UpdatedByGUID.Should().NotBeNull();
        standard.UpdatedByGUID.Should().Be(currentUserGUId);
        standard.UpdateDate.Should().NotBeNull();
        standard.WeightedCoefficient.Should().Be(Command.WeightedCoefficient);
        standard.SortOrder.Should().Be(Command.SortOrder);

        standard.ZirMehvarGUID.Should().Be(zirMehvarGUID);
        standard.CreationDate.Should().Be(creationDate);
        standard.CreatedByGUID.Should().Be(createdByGUID);
        standard.IsDeleted.Should().Be(isDeleted);
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenEditSucceeds()
    {
        //Arrange
        var standard = StandardData.Create();
        StandardRepositoryMock.FindAsync(Id, Arg.Any<CancellationToken>()).Returns(standard);
        StandardRepositoryMock.IsTitleUniqueAsync(Id, standard.ZirMehvarGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);
        StandardRepositoryMock.IsCodeUnique(Id, standard.ZirMehvarGUID, Command.Code, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result<Guid> result = await handler.Handle(Command, default);

        //Assert
        await unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }
}

