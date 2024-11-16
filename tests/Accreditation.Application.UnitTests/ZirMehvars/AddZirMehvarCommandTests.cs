using Accreditation.Application.Abstractions.Authentication;
using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Application.Mehvars;
using Accreditation.Application.UnitTests.Mehvars;
using Accreditation.Application.ZirMehvars;
using Accreditation.Application.ZirMehvars.Add;
using Accreditation.Domain.Mehvars.Entities;
using Accreditation.Domain.ZirMehvars.Entities;
using FluentAssertions;
using Moq;
using NSubstitute;
using SharedKernel;

namespace Accreditation.Application.UnitTests.ZirMehvars;

public class AddZirMehvarCommandTests
{
    private static readonly DateTime Now = DateTime.Now;
    private static readonly Guid id = Guid.NewGuid();
    private static readonly AddZirMehvarCommand Command = new(id, "DUMMY_Tilte", 695, 12);

    private readonly AddZirMehvarCommandHandler handler;
    private readonly IZirMehvarRepository zirMehvarRepositoryMock;
    private readonly IMehvarRepository mehvarRepositoryMock;
    private readonly ICurrentUser userContextMock;
    private readonly IDateTimeProvider dateTimeProviderMock;
    private readonly IUnitOfWork unitOfWorkMock;

    public AddZirMehvarCommandTests()
    {
        zirMehvarRepositoryMock = Substitute.For<IZirMehvarRepository>();
        mehvarRepositoryMock = Substitute.For<IMehvarRepository>();
        userContextMock = Substitute.For<ICurrentUser>();
        dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        dateTimeProviderMock.Now.Returns(Now);
        unitOfWorkMock = Substitute.For<IUnitOfWork>();

        handler = new AddZirMehvarCommandHandler(zirMehvarRepositoryMock, userContextMock, dateTimeProviderMock, mehvarRepositoryMock, unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenAddSucceeds()
    {
        //Arrange
        var mehvar = MehvarData.Create(id);
        mehvarRepositoryMock.FindAsync(id, Arg.Any<CancellationToken>()).Returns(mehvar);
        zirMehvarRepositoryMock.IsTitleUniqueAsync(null, Command.MehvarGuid, Command.Title, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result<Guid> result = await handler.Handle(Command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallAddFromRepository_WhenAddSucceeds()
    {
        //Arrange
        var mehvar = MehvarData.Create(id);
        mehvarRepositoryMock.FindAsync(id, Arg.Any<CancellationToken>()).Returns(mehvar);
        zirMehvarRepositoryMock.IsTitleUniqueAsync(null, Command.MehvarGuid, Command.Title, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result<Guid> result = await handler.Handle(Command, default);

        //Assert
        zirMehvarRepositoryMock.Received(1).Add(Arg.Is<ZirMehvar>(x => x.GUID == result.Value));
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenAddSucceeds()
    {
        //Arrange
        var mehvar = MehvarData.Create(id);
        mehvarRepositoryMock.FindAsync(id, Arg.Any<CancellationToken>()).Returns(mehvar);
        zirMehvarRepositoryMock.IsTitleUniqueAsync(null, Command.MehvarGuid, Command.Title, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result<Guid> result = await handler.Handle(Command, default);

        //Assert
        await unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenTitleIsNotUnique()
    {
        //Arrange
        var mehvar = MehvarData.Create(id);
        mehvarRepositoryMock.FindAsync(id, Arg.Any<CancellationToken>()).Returns(mehvar);
        zirMehvarRepositoryMock.IsTitleUniqueAsync(null, Command.MehvarGuid, Command.Title, Arg.Any<CancellationToken>()).Returns(false);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(ZirMehvarErrors.TitleNotUnique);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handel_Should_ReturnFailure_WhenMehvarisNotExist()
    {
        //arragnge
        var mehvarRepositoryMock = new Mock<IMehvarRepository>();
        mehvarRepositoryMock
           .Setup(repo => repo.FindAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
         .ReturnsAsync((Mehvar)null);
        //act
        var result = await handler.Handle(Command, default);

        //assert
        result.Error.Should().Be(MehvarErrors.NotFound);
        result.IsFailure.Should().BeTrue();
    }
}
