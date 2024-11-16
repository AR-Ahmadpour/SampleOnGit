using Accreditation.Application.Abstractions.Authentication;
using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.Standards;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Application.Standards;
using Accreditation.Application.Standards.Add;
using Accreditation.Application.UnitTests.ZirMehvars;
using Accreditation.Application.ZirMehvars;
using Accreditation.Domain.Standards.Entities;
using Accreditation.Domain.ZirMehvars.Entities;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace Accreditation.Application.UnitTests.Standards;

public class AddStandardCommandTests
{
    private static readonly DateTime Now = DateTime.Now;
    private static readonly Guid id = Guid.NewGuid();
    private static readonly AddStandardCommand Command = new(id, "DUMMY_Tilte", "DUMMY_ShortTilte",
             "34534", 45, 69);

    private readonly AddStandardCommandHandler handler;
    private readonly IStandardRepository StandardRepositoryMock;
    private readonly IZirMehvarRepository ZirMehvarRepositoryMock;
    private readonly ICurrentUser userContextMock;
    private readonly IDateTimeProvider dateTimeProviderMock;
    private readonly IUnitOfWork unitOfWorkMock;

    public AddStandardCommandTests()
    {
        StandardRepositoryMock = Substitute.For<IStandardRepository>();
        ZirMehvarRepositoryMock = Substitute.For<IZirMehvarRepository>();
        userContextMock = Substitute.For<ICurrentUser>();
        dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        dateTimeProviderMock.Now.Returns(Now);
        unitOfWorkMock = Substitute.For<IUnitOfWork>();

        handler = new AddStandardCommandHandler(StandardRepositoryMock, userContextMock, dateTimeProviderMock, ZirMehvarRepositoryMock, unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenTitleIsNotUnique()
    {
        //Arrange
        var mehvar = ZirMehvarData.Create(id);
        ZirMehvarRepositoryMock.FindAsync(id, Arg.Any<CancellationToken>()).Returns(mehvar);

        StandardRepositoryMock.IsTitleUniqueAsync(null, Command.ZirMehvarGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(false);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(StandardErrors.TitleNotUnique);
        result.IsFailure.Should().BeTrue();
    }


    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenCodeIsNotUnique()
    {
        //Arrange
        var mehvar = ZirMehvarData.Create(id);
        ZirMehvarRepositoryMock.FindAsync(id, Arg.Any<CancellationToken>()).Returns(mehvar);
        StandardRepositoryMock.IsTitleUniqueAsync(null, Command.ZirMehvarGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);
        StandardRepositoryMock.IsCodeUnique(null, Command.ZirMehvarGUID, Command.Code, Arg.Any<CancellationToken>()).Returns(false);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(StandardErrors.CodeNotUnique);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenAddSucceeds()
    {
        //Arrange
        var mehvar = ZirMehvarData.Create(id);
        ZirMehvarRepositoryMock.FindAsync(id, Arg.Any<CancellationToken>()).Returns(mehvar);
        StandardRepositoryMock.IsTitleUniqueAsync(null, Command.ZirMehvarGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);
        StandardRepositoryMock.IsCodeUnique(null, Command.ZirMehvarGUID, Command.Code, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result<Guid> result = await handler.Handle(Command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallAddFromRepository_WhenAddSucceeds()
    {
        //Arrange
        var mehvar = ZirMehvarData.Create(id);
        ZirMehvarRepositoryMock.FindAsync(id, Arg.Any<CancellationToken>()).Returns(mehvar);
        StandardRepositoryMock.IsTitleUniqueAsync(null, Command.ZirMehvarGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);
        StandardRepositoryMock.IsCodeUnique(null, Command.ZirMehvarGUID, Command.Code, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result<Guid> result = await handler.Handle(Command, default);

        //Assert
        StandardRepositoryMock.Received(1).Add(Arg.Is<Standard>(x => x.GUID == result.Value));
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenAddSucceeds()
    {
        //Arrange
        var mehvar = ZirMehvarData.Create(id);
        ZirMehvarRepositoryMock.FindAsync(id, Arg.Any<CancellationToken>()).Returns(mehvar);
        StandardRepositoryMock.IsTitleUniqueAsync(null, Command.ZirMehvarGUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);
        StandardRepositoryMock.IsCodeUnique(null, Command.ZirMehvarGUID, Command.Code, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        Result<Guid> result = await handler.Handle(Command, default);

        //Assert
        await unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenZirmehvarnotFound()
    {
        //Arrange
        ZirMehvarRepositoryMock.FindAsync(id, Arg.Any<CancellationToken>()).Returns((ZirMehvar)null);

        //Act
        var result = await handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(ZirMehvarErrors.NotFound);
        result.IsFailure.Should().BeTrue();
    }
}

