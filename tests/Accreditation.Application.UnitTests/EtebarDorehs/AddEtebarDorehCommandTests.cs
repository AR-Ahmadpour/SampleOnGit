//using Accreditation.Application.Abstractions.Authentication;
//using Accreditation.Application.Abstractions.Data;
//using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
//using Accreditation.Application.Common.Interfaces.Services;
//using Accreditation.Application.EtebarDorehs;
//using Accreditation.Application.EtebarDorehs.Add;
//using Accreditation.Domain.EtebarDorehs.Entities;
//using FluentAssertions;
//using NSubstitute;
//using SharedKernel;

//namespace Accreditation.Application.UnitTests.EtebarDorehs;

//public class AddEtebarDorehCommandTests
//{
//    private static readonly DateTime Now = DateTime.Now;
//    private static readonly AddEtebarDorehCommand Command = new(Guid.NewGuid(), "DUMMY_Tilte",
//             new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 1), true, 69);

//    private readonly AddEtebarDorehCommandHandler handler;
//    private readonly IEtebarDorehRepository etebarDorehRepositoryMock;
//    private readonly ICurrentUser userContextMock;
//    private readonly IDateTimeProvider dateTimeProviderMock;
//    private readonly IUnitOfWork unitOfWorkMock;

//    public AddEtebarDorehCommandTests()
//    {
//        etebarDorehRepositoryMock = Substitute.For<IEtebarDorehRepository>();
//        userContextMock = Substitute.For<ICurrentUser>();
//        dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
//        dateTimeProviderMock.Now.Returns(Now);
//        unitOfWorkMock = Substitute.For<IUnitOfWork>();

//        handler = new AddEtebarDorehCommandHandler(etebarDorehRepositoryMock, userContextMock, dateTimeProviderMock, unitOfWorkMock);
//    }

//    //[Fact]
//    //public async Task Handle_Should_ReturnFailure_WhenTitleIsNotUnique()
//    //{
//    //    //Arrange
//    //    etebarDorehRepositoryMock.IsTitleUniqueAsync(null, Command.Title, Arg.Any<CancellationToken>()).Returns(false);

//    //    //Act
//    //    var result = await handler.Handle(Command, default);

//    //    //Assert
//    //    result.Error.Should().Be(EtebarDorehErrors.TitleNotUnique);
//    //    result.IsFailure.Should().BeTrue();
//    //}

//    //[Fact]
//    //public async Task Handle_Should_Channge_AlreadyActivedEtebarDoreh_False()
//    //{
//    //    //Arrange
//    //    var etebarDoreAlredy = EtebarDoreh.Create(Guid.NewGuid(), "DUMMY_Tilte", new DateTime(2024, 1, 1), Guid.NewGuid(),
//    //         new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 1), true, 6);
//    //    etebarDorehRepositoryMock.FindCurrentEtebarDorehAsync(Command.OrgTypeGUID,Arg.Any<CancellationToken>()).Returns(etebarDoreAlredy);
//    //    etebarDorehRepositoryMock.IsTitleUniqueAsync(null, Command.Title, Arg.Any<CancellationToken>()).Returns(true);
//    //    etebarDorehRepositoryMock.IsThereAlreadyAnActiveEtebarDoreAsync(null, Command.IsCurrent, Arg.Any<CancellationToken>()).Returns(true);

//    //    //Act
//    //    var result = await handler.Handle(Command, default);

//    //    //Assert
//    //    etebarDoreAlredy.IsCurrent.Should().BeFalse();
//    //}

//    [Fact]
//    public async Task Handle_Should_CallAddFromRepository_WhenAddSucceeds()
//    {
//        //Arrange
//        etebarDorehRepositoryMock.IsTitleUniqueAsync(null, Command.Title, Arg.Any<CancellationToken>()).Returns(true);
//        etebarDorehRepositoryMock.IsThereAlreadyAnActiveEtebarDoreAsync(null, Command.IsCurrent, Arg.Any<CancellationToken>()).Returns(false);

//        //Act
//        Result<Guid> result = await handler.Handle(Command, default);

//        //Assert
//        etebarDorehRepositoryMock.Received(1).Add(Arg.Is<EtebarDoreh>(x => x.GUID == result.Value));
//        result.IsSuccess.Should().BeTrue();
//    }

//    [Fact]
//    public async Task Handle_Should_CallUnitOfWork_WhenAddSucceeds()
//    {
//        //Arrange
//        etebarDorehRepositoryMock.IsTitleUniqueAsync(null, Command.Title, Arg.Any<CancellationToken>()).Returns(true);
//        etebarDorehRepositoryMock.IsThereAlreadyAnActiveEtebarDoreAsync(null, Command.IsCurrent, Arg.Any<CancellationToken>()).Returns(false);

//        //Act
//        Result<Guid> result = await handler.Handle(Command, default);

//        //Assert
//        await unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
//        result.IsSuccess.Should().BeTrue();
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnSuccess_WhenAddSucceeds()
//    {
//        //Arrange
//        etebarDorehRepositoryMock.IsTitleUniqueAsync(null, Command.Title, Arg.Any<CancellationToken>()).Returns(true);
//        etebarDorehRepositoryMock.IsThereAlreadyAnActiveEtebarDoreAsync(null, Command.IsCurrent, Arg.Any<CancellationToken>()).Returns(false);

//        //Act
//        Result<Guid> result = await handler.Handle(Command, default);

//        //Assert
//        result.IsSuccess.Should().BeTrue();
//    }
//}
