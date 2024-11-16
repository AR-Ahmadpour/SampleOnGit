//using Accreditation.Application.Abstractions.Data;
//using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
//using Accreditation.Application.Common.Interfaces.Services;
//using Accreditation.Application.EtebarDorehs;
//using Accreditation.Application.EtebarDorehs.Edit;
//using Accreditation.Domain.EtebarDorehs.Entities;
//using FluentAssertions;
//using NSubstitute;
//using SharedKernel;

//namespace Accreditation.Application.UnitTests.EtebarDorehs;

//public class EditEtebarDorehCommandTests
//{
//    private static readonly DateTime Now = DateTime.Now;
//    private EditEtebarDorehCommand Command = new(Guid.NewGuid(), Guid.NewGuid(), "DUMMY_Tilte_Test", DateTime.Now.ToDateOnly(), DateTime.Now.Date.ToDateOnly(), true, 65);

//    private readonly EditEtebarDorehCommandHandler handler;
//    private readonly IEtebarDorehRepository EtebarDorehRepositoryMock;
//    private readonly ICurrentUser userContextMock;
//    private readonly IDateTimeProvider dateTimeProviderMock;
//    private readonly IUnitOfWork unitOfWorkMock;

//    public EditEtebarDorehCommandTests()
//    {
//        EtebarDorehRepositoryMock = Substitute.For<IEtebarDorehRepository>();
//        userContextMock = Substitute.For<ICurrentUser>();
//        dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
//        dateTimeProviderMock.Now.Returns(Now);
//        unitOfWorkMock = Substitute.For<IUnitOfWork>();

//        handler = new EditEtebarDorehCommandHandler(EtebarDorehRepositoryMock, userContextMock, dateTimeProviderMock, unitOfWorkMock);
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnFailure_WhenEtebarDorehNotExist()
//    {
//        //Arrange
//        EtebarDorehRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns((EtebarDoreh?)null);

//        //Act
//        var result = await handler.Handle(Command, default);

//        //Assert
//        result.Error.Should().Be(EtebarDorehErrors.NotFound);
//        result.IsFailure.Should().BeTrue();
//    }

//    //[Fact]
//    //public async Task Handle_Should_ReturnFailure_WhenTitleIsNotUnique()
//    //{
//    //    //Arrange
//    //    var etebarDoreh = EtebarDorehData.Create();
//    //    EtebarDorehRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(etebarDoreh);
//    //    EtebarDorehRepositoryMock.IsTitleUniqueAsync(Command.GUID, Command.Title, Arg.Any<CancellationToken>()).Returns(false);

//    //    //Act
//    //    var result = await handler.Handle(Command, default);

//    //    //Assert
//    //    result.Error.Should().Be(EtebarDorehErrors.TitleNotUnique);
//    //    result.IsFailure.Should().BeTrue();
//    //}

//    [Fact]
//    public async Task Handle_Should_ReturnFailure_WhenThereIsAlreadyAnActiveEtebarDore()
//    {
//        //Arrange
//        var etebarDoreh = EtebarDorehData.Create();
//        var etebarDoreAlredy = EtebarDoreh.Create(Guid.NewGuid(), "DUMMY_Tilte", new DateTime(2024, 1, 1), Guid.NewGuid(),
//            new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 1), true, 6);
//        EtebarDorehRepositoryMock.FindCurrentEtebarDorehAsync(Command.OrgTypeGUID, Arg.Any<CancellationToken>()).Returns(etebarDoreAlredy);
//        EtebarDorehRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(etebarDoreh);
//        EtebarDorehRepositoryMock.IsTitleUniqueAsync(Command.GUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);
//        EtebarDorehRepositoryMock.IsThereAlreadyAnActiveEtebarDoreAsync(Command.GUID, true, Arg.Any<CancellationToken>()).Returns(true);

//        //Act
//        var result = await handler.Handle(Command, default);

//        //Assert
//        etebarDoreAlredy.IsCurrent.Should().BeFalse();
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnSuccess_WhenEditSucceeds()
//    {
//        //Arrange
//        var etebarDoreh = EtebarDorehData.Create();
//        var creationDate = etebarDoreh.CreationDate;
//        var createdByGUID = etebarDoreh.CreatedByGUID;
//        var isDeleted = etebarDoreh.IsDeleted;
//        EtebarDorehRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(etebarDoreh);
//        EtebarDorehRepositoryMock.IsTitleUniqueAsync(Command.GUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);
//        EtebarDorehRepositoryMock.IsThereAlreadyAnActiveEtebarDoreAsync(etebarDoreh.GUID, true, Arg.Any<CancellationToken>()).Returns(false);
//        var currentUserGUId = Guid.NewGuid().ToString();
//        userContextMock.UserId.Returns(currentUserGUId);

//        //Act
//        Result<Guid> result = await handler.Handle(Command, default);

//        //Assert
//        result.IsSuccess.Should().BeTrue();
//        result.Value.Should().Be(etebarDoreh.GUID);
//        etebarDoreh.OrgTypeGUID.Should().Be(Command.OrgTypeGUID);
//        etebarDoreh.Title.Should().Be(Command.Title);
//        etebarDoreh.UpdatedByGUID.Should().NotBeNull();
//        etebarDoreh.UpdatedByGUID.Should().Be(currentUserGUId);
//        etebarDoreh.UpdateDate.Should().NotBeNull();
//        etebarDoreh.StartDate.Should().Be(Command.StartDate);
//        etebarDoreh.EndDate.Should().Be(Command.EndDate);
//        etebarDoreh.IsCurrent.Should().Be(Command.IsCurrent);
//        etebarDoreh.SortOrder.Should().Be(Command.SortOrder);

//        etebarDoreh.CreationDate.Should().Be(creationDate);
//        etebarDoreh.CreatedByGUID.Should().Be(createdByGUID);
//        etebarDoreh.IsDeleted.Should().Be(isDeleted);
//    }

//    [Fact]
//    public async Task Handle_Should_CallUnitOfWork_WhenEditSucceeds()
//    {
//        //Arrange
//        var etebarDoreh = EtebarDorehData.Create();
//        EtebarDorehRepositoryMock.FindAsync(Command.GUID, Arg.Any<CancellationToken>()).Returns(etebarDoreh);
//        EtebarDorehRepositoryMock.IsTitleUniqueAsync(Command.GUID, Command.Title, Arg.Any<CancellationToken>()).Returns(true);

//        //Act
//        Result<Guid> result = await handler.Handle(Command, default);

//        //Assert
//        await unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
//        result.IsSuccess.Should().BeTrue();
//    }
//}

