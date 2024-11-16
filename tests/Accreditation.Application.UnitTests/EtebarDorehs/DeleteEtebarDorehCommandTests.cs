using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.EtebarDorehs;
using Accreditation.Application.EtebarDorehs.Delete;
using Accreditation.Domain.EtebarDorehs.Entities;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace AccrDeleteation.Application.UnitTests.EtebarDorehs;

public class DeleteEtebarDorehCommandTests
{
    private static readonly DeleteEtebarDorehCommand Command = new(Guid.NewGuid());

    private readonly DeleteEtebarDorehCommandHandler handler;
    private readonly IEtebarDorehRepository EtebarDorehRepositoryMock;
    private readonly IUnitOfWork unitOfWorkMock;

    public DeleteEtebarDorehCommandTests()
    {
        EtebarDorehRepositoryMock = Substitute.For<IEtebarDorehRepository>();
        unitOfWorkMock = Substitute.For<IUnitOfWork>();

        handler = new DeleteEtebarDorehCommandHandler(EtebarDorehRepositoryMock, unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenEtebarDorehNotExist()
    {
        //Arrange
        EtebarDorehRepositoryMock.AnyAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(false);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(EtebarDorehErrors.NotFound);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallDelete_WhenDeleteSucceeds()
    {
        //Arrange
        EtebarDorehRepositoryMock.AnyAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        EtebarDorehRepositoryMock.Received(1).Delete(Arg.Is<EtebarDoreh>(x => x.GUID == Command.GUID));
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenDeleteSucceeds()
    {
        //Arrange
        EtebarDorehRepositoryMock.AnyAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(true);

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
        EtebarDorehRepositoryMock.AnyAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}

