using Accreditation.Application.Abstractions;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.IntegrationTests;
using Accreditation.Application.Mehvars.Add;
using Accreditation.Application.Mehvars.GetList;
using Accreditation.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Accredetation.Application.IntegrationTests.Mehvars;

[TestCaseOrderer("Accredetation.Application.IntegrationTests.TestCaseOrderer", "MehvarsAssembly")]

public class MehvarTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public MehvarTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;

        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AccreditationDbContext>();
            SeedTestData.Initialize(context);
        }
    }

    private AddMehvarCommand CreateCommand(Guid EtebarDorehGuid, string title)
    {
        return new AddMehvarCommand(
            EtebarDorehGuid,
            title,
            10,
            1);
    }


    //[Fact, Order(5)]
    //public async Task Handle_AddsNewMehvar_ReturnsSuccess()
    //{
    //    // Arrange
    //    var orgTypeGuid = SeedTestData.OrgGuid;
    //    var etebarDorehGuid = SeedTestData.EtebarDorehGuid;

    //    var command = CreateCommand(etebarDorehGuid, "MehvarTitle1");

    //    using (var scope = _factory.Services.CreateScope())
    //    {
    //        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<AddMehvarCommand, Guid>>();

    //        // Act
    //        var result = await handler.Handle(command, CancellationToken.None);

    //        // Assert
    //        Assert.True(result.IsSuccess);
    //        Assert.NotEqual(Guid.Empty, result.Value);
    //    }
    //}


    //[Fact, Order(6)]
    //public async Task GetMehvarsList_Successfull()
    //{
    //    // Arrange
    //    var orgTypeGuid = SeedTestData.OrgGuid;
    //    var etebarDorehGuid = SeedTestData.EtebarDorehGuid;

    //    var command = CreateCommand(etebarDorehGuid, "MehvarTitle2");

    //    using (var scope = _factory.Services.CreateScope())
    //    {
    //        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<AddMehvarCommand, Guid>>();
    //        var resultCommand = await handler.Handle(command, CancellationToken.None);
    //        var queryHandler = scope.ServiceProvider.GetRequiredService<IQueryHandler<GetListMehvarQuery, IPaginator<GetListMehvarResponse>>>();

    //        // Act
    //        var query = new GetListMehvarQuery(etebarDorehGuid, 1, 10);
    //        var result = await queryHandler.Handle(query, CancellationToken.None);
    //        var paginatorItems = await result.Value.GetItems();

    //        // Assert
    //        Assert.NotNull(paginatorItems);
    //        Assert.NotEmpty(paginatorItems);
    //        Assert.True(paginatorItems.Count <= 10);
    //        var item = paginatorItems.FirstOrDefault(i => i.Title == "MehvarTitle2");
    //        Assert.NotNull(item);
    //        Assert.Equal("MehvarTitle2", item.Title);
    //    }
    //}
}



