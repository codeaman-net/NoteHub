using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace NoteHub.Infrastructure.Persistence;

public class DatabaseContext(IConfiguration configuration)
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                                                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}