using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using Accreditation.Application.UnitTests.ZirMehvars;
using Accreditation.Application.ZirMehvars;
using Accreditation.Application.ZirMehvars.LogicalDelete;
using Accreditation.Domain.ZirMehvars.Entities;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace AccrLogicalDeleteation.Application.UnitTests.ZirMehvars;

public class LogicalDeleteZirMehvarCommandTests
{
    private static readonly DateTime Now = DateTime.Now;
    private static readonly LogicalDeleteZirMehvarCommand Command = new(Guid.NewGuid());

    private readonly LogicalDeleteZirMehvarCommandHandler handler;
    private readonly IZirMehvarRepository ZirMehvarRepositoryMock;
    private readonly IUnitOfWork unitOfWorkMock;

    public LogicalDeleteZirMehvarCommandTests()
    {
        ZirMehvarRepositoryMock = Substitute.For<IZirMehvarRepository>();
        unitOfWorkMock = Substitute.For<IUnitOfWork>();

        handler = new LogicalDeleteZirMehvarCommandHandler(ZirMehvarRepositoryMock, unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenZirMehvarNotExist()
    {
        //Arrange
        ZirMehvarRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns((ZirMehvar?)null);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(ZirMehvarErrors.NotFound);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenLogicalDeleteSucceeds()
    {
        //Arrange
        var ZirMehvar = ZirMehvarData.Create();
        ZirMehvarRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(ZirMehvar);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        ZirMehvar.IsDeleted.Should().BeTrue();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenLogicalDeleteSucceeds()
    {
        //Arrange
        var ZirMehvar = ZirMehvarData.Create();
        ZirMehvarRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(ZirMehvar);

        //Act
        Result result = await handler.Handle(Command, default);

        //Assert
        await unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }
}

