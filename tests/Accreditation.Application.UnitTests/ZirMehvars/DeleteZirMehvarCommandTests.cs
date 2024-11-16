using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using Accreditation.Application.ZirMehvars;
using Accreditation.Application.ZirMehvars.Delete;
using Accreditation.Domain.ZirMehvars.Entities;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace AccrDeleteation.Application.UnitTests.ZirMehvars;

public class DeleteZirMehvarCommandTests
{
    private static readonly DeleteZirMehvarCommand Command = new(Guid.NewGuid());

    private readonly DeleteZirMehvarCommandHandler handler;
    private readonly IZirMehvarRepository ZirMehvarRepositoryMock;
    private readonly IUnitOfWork unitOfWorkMock;

    public DeleteZirMehvarCommandTests()
    {
        ZirMehvarRepositoryMock = Substitute.For<IZirMehvarRepository>();
        unitOfWorkMock = Substitute.For<IUnitOfWork>();

        handler = new DeleteZirMehvarCommandHandler(ZirMehvarRepositoryMock, unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenZirMehvarNotExist()
    {
        //Arrange
        ZirMehvarRepositoryMock.AnyAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(false);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(ZirMehvarErrors.NotFound);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallDelete_WhenDeleteSucceeds()
    {
        //Arrange
        ZirMehvarRepositoryMock.AnyAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        ZirMehvarRepositoryMock.Received(1).Delete(Arg.Is<ZirMehvar>(x => x.GUID == Command.GUID));
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenDeleteSucceeds()
    {
        //Arrange
        ZirMehvarRepositoryMock.AnyAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        await unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenDeleteSucceeds()
    {
        //Arrange
        ZirMehvarRepositoryMock.AnyAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}

