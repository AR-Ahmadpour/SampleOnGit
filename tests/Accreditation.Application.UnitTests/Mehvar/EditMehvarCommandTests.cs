using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Application.Mehvars;
using Accreditation.Application.Mehvars.Edit;
using Accreditation.Domain.Mehvars.Entities;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace Accreditation.Application.UnitTests.Mehvars;

public class EditMehvarCommandTests
{
    private static readonly DateTime Now = DateTime.Now;
    private static readonly EditMehvarCommand Command = new(Guid.NewGuid(), "DUMMY_Tilte", 65, 54);

    private readonly EditMehvarCommandHandler handler;
    private readonly IMehvarRepository MehvarRepositoryMock;
    private readonly ICurrentUser userContextMock;
    private readonly IDateTimeProvider dateTimeProviderMock;
    private readonly IUnitOfWork unitOfWorkMock;

    public EditMehvarCommandTests()
    {
        MehvarRepositoryMock = Substitute.For<IMehvarRepository>();
        userContextMock = Substitute.For<ICurrentUser>();
        dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        dateTimeProviderMock.Now.Returns(Now);
        unitOfWorkMock = Substitute.For<IUnitOfWork>();

        handler = new EditMehvarCommandHandler(MehvarRepositoryMock, userContextMock, dateTimeProviderMock, unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenMehvarNotExist()
    {
        //Arrange
        MehvarRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns((Mehvar?)null);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(MehvarErrors.NotFound);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenTitleIsNotUnique()
    {
        //Arrange
        var mehvar = MehvarData.Create();
        MehvarRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(mehvar);
        //MehvarRepositoryMock.IsTitleUniqueAsync(Command.GUID, Command.Title, Arg.Any<CancellationToken>()).Returns(false);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(MehvarErrors.TitleNotUnique);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenEditSucceeds()
    {
        //Arrange
        var mehvar = MehvarData.Create();
        var creationDate = mehvar.CreationDate;
        var createdByGUID = mehvar.CreatedByGUID;
        var isDeleted = mehvar.IsDeleted;
        MehvarRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(mehvar);
        //MehvarRepositoryMock.IsTitleUniqueAsync(Command.GUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);
        var currentUserGUId = Guid.NewGuid().ToString();
        userContextMock.UserId.Returns(currentUserGUId);
        //Act
        Result<Guid> result = await handler.Handle(Command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(mehvar.GUID);
        mehvar.Title.Should().Be(Command.Title);
        mehvar.UpdatedByGUID.Should().NotBeNull();
        mehvar.UpdatedByGUID.Should().Be(currentUserGUId);
        mehvar.UpdateDate.Should().NotBeNull();
        mehvar.WeightedCoefficient.Should().Be(Command.WeightedCoefficient);
        mehvar.SortOrder.Should().Be(Command.SortOrder);

        mehvar.CreationDate.Should().Be(creationDate);
        mehvar.CreatedByGUID.Should().Be(createdByGUID);
        mehvar.IsDeleted.Should().Be(isDeleted);
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenEditSucceeds()
    {
        //Arrange
        var mehvar = MehvarData.Create();
        MehvarRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(mehvar);
        //MehvarRepositoryMock.IsTitleUniqueAsync(Command.GUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result<Guid> result = await handler.Handle(Command, default);

        //Assert
        await unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }
}

