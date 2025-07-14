using Microsoft.Data.SqlClient;

namespace TenderManagementSystem.Infrastructure.Data.Dapper
{
    public interface IDapperContext
    {
        SqlConnection CreateConnection();
    }

    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
        }
    }
}