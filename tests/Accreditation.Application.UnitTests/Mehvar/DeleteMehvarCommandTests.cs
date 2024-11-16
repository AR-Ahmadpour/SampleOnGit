using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Application.Mehvars;
using Accreditation.Application.Mehvars.Delete;
using Accreditation.Domain.Mehvars.Entities;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace AccrDeleteation.Application.UnitTests.Mehvars;

public class DeleteMehvarCommandTests
{
    private static readonly DeleteMehvarCommand Command = new(Guid.NewGuid());

    private readonly DeleteMehvarCommandHandler handler;
    private readonly IMehvarRepository MehvarRepositoryMock;
    private readonly IUnitOfWork unitOfWorkMock;

    public DeleteMehvarCommandTests()
    {
        MehvarRepositoryMock = Substitute.For<IMehvarRepository>();
        unitOfWorkMock = Substitute.For<IUnitOfWork>();

        handler = new DeleteMehvarCommandHandler(MehvarRepositoryMock, unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenMehvarNotExist()
    {
        //Arrange
        MehvarRepositoryMock.AnyAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(false);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(MehvarErrors.NotFound);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallDelete_WhenDeleteSucceeds()
    {
        //Arrange
        MehvarRepositoryMock.AnyAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        MehvarRepositoryMock.Received(1).Delete(Arg.Is<Mehvar>(x => x.GUID == Command.GUID));
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenDeleteSucceeds()
    {
        //Arrange
        MehvarRepositoryMock.AnyAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(true);

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
        MehvarRepositoryMock.AnyAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}

