using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.Standards;
using Accreditation.Application.Standards;
using Accreditation.Application.Standards.Delete;
using Accreditation.Domain.Standards.Entities;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace AccrDeleteation.Application.UnitTests.Standards;

public class DeleteStandardCommandTests
{
    private static readonly DeleteStandardCommand Command = new(Guid.NewGuid());

    private readonly DeleteStandardCommandHandler handler;
    private readonly IStandardRepository StandardRepositoryMock;
    private readonly IUnitOfWork unitOfWorkMock;

    public DeleteStandardCommandTests()
    {
        StandardRepositoryMock = Substitute.For<IStandardRepository>();
        unitOfWorkMock = Substitute.For<IUnitOfWork>();

        handler = new DeleteStandardCommandHandler(StandardRepositoryMock, unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenStandardNotExist()
    {
        //Arrange
        StandardRepositoryMock.AnyAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(false);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(StandardErrors.NotFound);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallDelete_WhenDeleteSucceeds()
    {
        //Arrange
        StandardRepositoryMock.AnyAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        StandardRepositoryMock.Received(1).Delete(Arg.Is<Standard>(x => x.GUID == Command.GUID));
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenDeleteSucceeds()
    {
        //Arrange
        StandardRepositoryMock.AnyAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(true);

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
        StandardRepositoryMock.AnyAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}

