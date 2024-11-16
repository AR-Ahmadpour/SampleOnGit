using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Application.EtebarDorehs.LogicalDelete;
using Accreditation.Application.Mehvars;
using Accreditation.Application.UnitTests.Mehvars;
using Accreditation.Domain.Mehvars.Entities;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace AccrLogicalDeleteation.Application.UnitTests.Mehvars;

public class LogicalDeleteMehvarCommandTests
{
    private static readonly DateTime Now = DateTime.Now;
    private static readonly LogicalDeleteMehvarCommand Command = new(Guid.NewGuid());

    private readonly LogicalDeleteMehvarCommandHandler handler;
    private readonly IMehvarRepository MehvarRepositoryMock;
    private readonly IUnitOfWork unitOfWorkMock;

    public LogicalDeleteMehvarCommandTests()
    {
        MehvarRepositoryMock = Substitute.For<IMehvarRepository>();
        unitOfWorkMock = Substitute.For<IUnitOfWork>();

        handler = new LogicalDeleteMehvarCommandHandler(MehvarRepositoryMock, unitOfWorkMock);
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
    public async Task Handle_Should_ReturnSuccess_WhenLogicalDeleteSucceeds()
    {
        //Arrange
        var Mehvar = MehvarData.Create();
        MehvarRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(Mehvar);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        Mehvar.IsDeleted.Should().BeTrue();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenLogicalDeleteSucceeds()
    {
        //Arrange
        var Mehvar = MehvarData.Create();
        MehvarRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(Mehvar);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        await unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }
}

