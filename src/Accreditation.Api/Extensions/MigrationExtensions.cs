using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using AccreditationDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<AccreditationDbContext>();

        dbContext.Database.Migrate();
    }
}

