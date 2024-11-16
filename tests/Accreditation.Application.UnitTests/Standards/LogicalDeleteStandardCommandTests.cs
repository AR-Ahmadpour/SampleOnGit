using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.Standards;
using Accreditation.Application.Standards;
using Accreditation.Application.Standards.LogicalDelete;
using Accreditation.Application.UnitTests.Standards;
using Accreditation.Domain.Standards.Entities;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace AccrLogicalDeleteation.Application.UnitTests.Standards;

public class LogicalDeleteStandardCommandTests
{
    private static readonly DateTime Now = DateTime.Now;
    private static readonly LogicalDeleteStandardCommand Command = new(Guid.NewGuid());

    private readonly LogicalDeleteStandardCommandHandler handler;
    private readonly IStandardRepository StandardRepositoryMock;
    private readonly IUnitOfWork unitOfWorkMock;

    public LogicalDeleteStandardCommandTests()
    {
        StandardRepositoryMock = Substitute.For<IStandardRepository>();
        unitOfWorkMock = Substitute.For<IUnitOfWork>();

        handler = new LogicalDeleteStandardCommandHandler(StandardRepositoryMock, unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenStandardNotExist()
    {
        //Arrange
        StandardRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns((Standard?)null);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(StandardErrors.NotFound);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenLogicalDeleteSucceeds()
    {
        //Arrange
        var Standard = StandardData.Create();
        StandardRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(Standard);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        Standard.IsDeleted.Should().BeTrue();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenLogicalDeleteSucceeds()
    {
        //Arrange
        var Standard = StandardData.Create();
        StandardRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(Standard);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        await unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }
}

