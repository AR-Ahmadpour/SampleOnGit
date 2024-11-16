using Accreditation.Application.Abstractions.Authentication;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Domain.Users;
using Accreditation.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Accreditation.Application.IntegrationTests;
public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.Test.json");

            config.AddJsonFile(configPath);
        });

        builder.ConfigureServices(services =>
        {
            // Remove existing DbContext registration
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AccreditationDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Register test DbContext
            services.AddDbContext<AccreditationDbContext>(options =>
            {
                options.UseSqlServer("server=192.168.1.9;database=AccreditationTest;user Id=sa;password=123456;TrustServerCertificate=True");
            });

            // Register IUserContext with a mock or test implementation
            //services.AddScoped<ICurrentUser, MockUserContext>();

            // Register IAuthenticationService with a mock or test implementation
            services.AddScoped<IAuthenticationService, MockAuthenticationService>(); // Add this line

            // Add MediatR and register handlers
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            //    typeof(ICommandHandler<>).Assembly));
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                //config.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));
            });


            // Ensure service provider is built
            var sp = services.BuildServiceProvider();

            // Seed test database
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AccreditationDbContext>();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                try
                {
                    SeedData(db);
                }
                catch (Exception ex)
                {
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                    logger.LogError(ex, "An error occurred seeding the database. Error: {Message}", ex.Message);
                }
            }
        });
    }

    private void SeedData(AccreditationDbContext context)
    {
    }
}

// Mock implementation of IUserContext for testing
//public class MockUserContext : ICurrentUser
//{
//    public string? UserId => Guid.NewGuid().ToString();

//    public string? Roles => throw new NotImplementedException();
//}

// Mock implementation of IAuthenticationService for testing
public class MockAuthenticationService : IAuthenticationService
{
    // Implement IAuthenticationService members as needed
    public Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}




