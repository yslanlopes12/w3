using System.Data;
using Microsoft.Data.SqlClient;


namespace Infrastructure.Data
{
    public class SqlContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public SqlContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnections");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}