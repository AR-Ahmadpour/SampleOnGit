using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.EtebarDorehs;
using Accreditation.Application.EtebarDorehs.LogicalDelete;
using Accreditation.Application.UnitTests.EtebarDorehs;
using Accreditation.Domain.EtebarDorehs.Entities;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace AccrLogicalDeleteation.Application.UnitTests.EtebarDorehs;

public class LogicalDeleteEtebarDorehCommandTests
{
    private static readonly DateTime Now = DateTime.Now;
    private static readonly LogicalDeleteEtebarDorehCommand Command = new(Guid.NewGuid());

    private readonly LogicalDeleteEtebarDorehCommandHandler handler;
    private readonly IEtebarDorehRepository EtebarDorehRepositoryMock;
    private readonly IUnitOfWork unitOfWorkMock;

    public LogicalDeleteEtebarDorehCommandTests()
    {
        EtebarDorehRepositoryMock = Substitute.For<IEtebarDorehRepository>();
        unitOfWorkMock = Substitute.For<IUnitOfWork>();

        handler = new LogicalDeleteEtebarDorehCommandHandler(EtebarDorehRepositoryMock, unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenEtebarDorehNotExist()
    {
        //Arrange
        EtebarDorehRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns((EtebarDoreh?)null);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(EtebarDorehErrors.NotFound);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenLogicalDeleteSucceeds()
    {
        //Arrange
        var etebarDoreh = EtebarDorehData.Create();
        EtebarDorehRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(etebarDoreh);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        etebarDoreh.IsDeleted.Should().BeTrue();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenLogicalDeleteSucceeds()
    {
        //Arrange
        var etebarDoreh = EtebarDorehData.Create();
        EtebarDorehRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(etebarDoreh);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        await unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }
}

