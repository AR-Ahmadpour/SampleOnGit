using Accredetation.Application.IntegrationTests;
using Accreditation.Application.Abstractions;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using Accreditation.Application.EtebarDorehs;
using Accreditation.Application.EtebarDorehs.Add;
using Accreditation.Application.EtebarDorehs.Edit;
using Accreditation.Application.EtebarDorehs.GetList;
using Accreditation.Domain.EtebarDorehs.Dtos;
using Accreditation.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Accreditation.Application.IntegrationTests.EtebarDorehs;

[TestCaseOrderer("Accredetation.Application.IntegrationTests.TestCaseOrderer", "EtebarDorehsAssembly")]
public class EtebarDorehTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public EtebarDorehTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AccreditationDbContext>();
            SeedTestData.Initialize(context);
        }
    }

    private AddEtebarDorehCommand CreateCommand(Guid orgTypeGuid, string title)
    {
        return new AddEtebarDorehCommand(
            orgTypeGuid,
            title,
            new DateOnly(2023, 6, 1),
            new DateOnly(2023, 7, 1),
            true,
            1
        );
    }

    [Fact, Order(1)]
    public async Task Handle_AddsNewEtebarDoreh_ReturnsSuccess()
    {
        // Arrange
        var orgTypeGuid = SeedTestData.OrgGuid;
        var command = CreateCommand(orgTypeGuid, "Test Title1");

        using (var scope = _factory.Services.CreateScope())
        {
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<AddEtebarDorehCommand, Guid>>();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEqual(Guid.Empty, result.Value);
        }
    }

    [Fact, Order(2)]
    public async Task Handle_AddsNewEtebarDoreh_withSameTitle_ReturnsError()
    {
        // Arrange
        var orgTypeGuid = SeedTestData.OrgGuid;
        var command1 = CreateCommand(orgTypeGuid, "Test Title2");
        var command2 = CreateCommand(orgTypeGuid, "Test Title2"); // Same title

        using (var scope = _factory.Services.CreateScope())
        {
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<AddEtebarDorehCommand, Guid>>();

            // Act
            var result1 = await handler.Handle(command1, CancellationToken.None);
            Assert.True(result1.IsSuccess);

            var result2 = await handler.Handle(command2, CancellationToken.None);

            // Assert
            Assert.True(result2.IsFailure);
            Assert.Equal(EtebarDorehErrors.TitleNotUnique, result2.Error);
        }
    }

    [Fact, Order(3)]
    public async Task GetEtebareDorehList_Successfull()
    {
        // Arrange
        var orgTypeGuid = SeedTestData.OrgGuid;
        var command = CreateCommand(orgTypeGuid, "Test Title3");

        using (var scope = _factory.Services.CreateScope())
        {
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<AddEtebarDorehCommand, Guid>>();
            var resultCommand = await handler.Handle(command, CancellationToken.None);
            var queryHandler = scope.ServiceProvider.GetRequiredService<IQueryHandler<GetListEtebarDorehQuery, PagedList<GetListDto>>>();

            // Act
            var query = new GetListEtebarDorehQuery(1, 10,(new Guid()));
            var result = await queryHandler.Handle(query, CancellationToken.None);
            var paginatorItems =  result.Value.Items;

            // Assert
            Assert.NotNull(paginatorItems);
            Assert.NotEmpty(paginatorItems);
            Assert.True(paginatorItems.Count <= 10);
            var item = paginatorItems.FirstOrDefault(i => i.Title == "Test Title3");
            Assert.NotNull(item);
            Assert.Equal("Test Title3", item.Title);
        }
    }

    [Fact, Order(4)]
    public async Task EditEtebareDoreh_Successfull()
    {
        // Arrange
        var orgTypeGuid = SeedTestData.OrgGuid;
        var command = CreateCommand(orgTypeGuid, "Test Title4");

        using (var scope = _factory.Services.CreateScope())
        {
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<AddEtebarDorehCommand, Guid>>();
            var resultCommand = await handler.Handle(command, CancellationToken.None);
            var queryHandler = scope.ServiceProvider.GetRequiredService<IQueryHandler<GetListEtebarDorehQuery, PagedList<GetListDto>>>();
            var query = new GetListEtebarDorehQuery(1, 10, (new Guid()));

            var editCommand = new EditEtebarDorehCommand(resultCommand.Value,
                                                         orgTypeGuid,
                                                         "EtebarDoreh5",
                                                         new DateOnly(2023, 6, 1),
                                                         new DateOnly(2023, 7, 1),
                                                         true,
                                                         1);
            var editHandler = scope.ServiceProvider.GetRequiredService<ICommandHandler<EditEtebarDorehCommand, Guid>>();


            // Act
            var resultEditCommand = await editHandler.Handle(editCommand, CancellationToken.None);

            // Assert

            var result = await queryHandler.Handle(query, CancellationToken.None);
            var paginatorItems = result.Value.Items;
            var item = paginatorItems.FirstOrDefault(i => i.Guid == resultCommand.Value);

            Assert.NotNull(result);
            Assert.Equal("EtebarDoreh5", item.Title);
        }
    }
}



