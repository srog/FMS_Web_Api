using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Fms_Web_Api
{
    public class FmsDbContext : DbContext
    {
        private readonly string _connectionString = "server=(localDB)\\MSSQLLocalDb; Integrated Security=True; database=FMS";
        public IDbConnection Connection;

        public FmsDbContext()
        {
            if (Connection == null)
            {
                Connection = new SqlConnection(_connectionString);
            }
        }
    }
}