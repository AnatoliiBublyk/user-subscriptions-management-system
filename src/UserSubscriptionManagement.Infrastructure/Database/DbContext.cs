using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace UserSubscriptionManagement.Infrastructure.Database;

public class DbContext
{
    private readonly string _connectionString;
    public DbContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}