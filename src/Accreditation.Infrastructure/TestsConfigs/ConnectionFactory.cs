using Accreditation.Infrastructure.Database;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.TestsConfigs
{
    public class ConnectionFactory : IDisposable
    {
        private readonly SqliteConnection _connection;

        private bool disposedValue = false;

        public ConnectionFactory()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
        }

        public AccreditationDbContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<AccreditationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            var context = new AccreditationDbContext(option);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        public AccreditationDbContext CreateContextForSQLite()
        {
            var option = new DbContextOptionsBuilder<AccreditationDbContext>()
                .UseSqlite(_connection).Options;

            var context = new AccreditationDbContext(option);

            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        //public DocumentDbContext CreateDocumentContextForSQLite()
        //{
        //    var option = new DbContextOptionsBuilder<DocumentDbContext>()
        //        .UseSqlite(_connection)
        //        .Options;

        //    var context = new DocumentDbContext(option);

        //    if (context != null)
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();
        //    }

        //    return context;
        //}

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
