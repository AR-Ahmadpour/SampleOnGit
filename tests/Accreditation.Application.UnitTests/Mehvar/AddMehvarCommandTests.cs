using Accreditation.Application.Abstractions.Authentication;
using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Application.EtebarDorehs;
using Accreditation.Application.Mehvars;
using Accreditation.Application.Mehvars.Add;
using Accreditation.Application.UnitTests.EtebarDorehs;
using Accreditation.Domain.Mehvars.Entities;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace Accreditation.Application.UnitTests.Mehvars;

public class AddMehvarCommandTests
{
    private static readonly DateTime Now = DateTime.Now;
    private static readonly Guid etebarDorehGuid = Guid.NewGuid();
    private static readonly AddMehvarCommand Command = new
         (etebarDorehGuid, "DUMMY_Tilte", 123, 23);

    private readonly AddMehvarCommandHandler handler;
    private readonly IMehvarRepository mehvarRepositoryMock;
    private readonly IEtebarDorehRepository etebarDorehRepositoryMock;
    private readonly ICurrentUser userContextMock;
    private readonly IDateTimeProvider dateTimeProviderMock;
    private readonly IUnitOfWork unitOfWorkMock;

    public AddMehvarCommandTests()
    {
        mehvarRepositoryMock = Substitute.For<IMehvarRepository>();
        userContextMock = Substitute.For<ICurrentUser>();
        etebarDorehRepositoryMock = Substitute.For<IEtebarDorehRepository>();
        dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        dateTimeProviderMock.Now.Returns(Now);
        unitOfWorkMock = Substitute.For<IUnitOfWork>();

        handler = new AddMehvarCommandHandler(mehvarRepositoryMock, userContextMock, dateTimeProviderMock, unitOfWorkMock, etebarDorehRepositoryMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenTitleIsNotUnique()
    {
        //Arrange
        var etebarDoreh = EtebarDorehData.Create(etebarDorehGuid);
        etebarDorehRepositoryMock.FindAsync(etebarDorehGuid, Arg.Any<CancellationToken>()).Returns(etebarDoreh);
        mehvarRepositoryMock.IsTitleUniqueAsync(null, Command.EtebarDorehGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(false);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(MehvarErrors.TitleNotUnique);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenAddSucceeds()
    {
        //Arrange
        var etebarDoreh = EtebarDorehData.Create(etebarDorehGuid);
        etebarDorehRepositoryMock.FindAsync(etebarDorehGuid, Arg.Any<CancellationToken>()).Returns(etebarDoreh);
        mehvarRepositoryMock.IsTitleUniqueAsync(null, Command.EtebarDorehGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result<Guid> result = await handler.Handle(Command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallAddFromRepository_WhenAddSucceeds()
    {
        //Arrange
        var etebarDoreh = EtebarDorehData.Create(etebarDorehGuid);
        etebarDorehRepositoryMock.FindAsync(etebarDorehGuid, Arg.Any<CancellationToken>()).Returns(etebarDoreh);
        mehvarRepositoryMock.IsTitleUniqueAsync(null, Command.EtebarDorehGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result<Guid> result = await handler.Handle(Command, default);

        //Assert
        mehvarRepositoryMock.Received(1).Add(Arg.Is<Mehvar>(x => x.GUID == result.Value));
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenAddSucceeds()
    {
        //Arrange
        var etebarDoreh = EtebarDorehData.Create(etebarDorehGuid);
        etebarDorehRepositoryMock.FindAsync(etebarDorehGuid, Arg.Any<CancellationToken>()).Returns(etebarDoreh);
        mehvarRepositoryMock.IsTitleUniqueAsync(null, Command.EtebarDorehGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result<Guid> result = await handler.Handle(Command, default);

        //Assert
        await unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }

    public async Task Handle_Should_ReturnFailure_WhenEtebarDorehNotFound()
    {
        //Arrange
        mehvarRepositoryMock.IsTitleUniqueAsync(null, Command.EtebarDorehGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result<Guid> result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(EtebarDorehErrors.NotFound);
        result.IsFailure.Should().BeTrue();
    }
}
