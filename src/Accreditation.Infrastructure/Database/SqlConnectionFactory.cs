using Accreditation.Application.Abstractions.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Accreditation.Infrastructure.Database;

internal sealed class SqlConnectionFactory(string _connectionString) : IDbConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}

