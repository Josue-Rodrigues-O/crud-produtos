using Ecommerce.Infrastructure.Configurations;
using Microsoft.Data.SqlClient;

namespace Ecommerce.Infrastructure.SqlServer
{
    public static class SqlServerInstance
    {
        public static SqlConnection Create(SqlServerConfig sqlServerConfigs)
        {
            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = sqlServerConfigs.DataSource,
                UserID = sqlServerConfigs.User,
                Password = sqlServerConfigs.Password,
                InitialCatalog = sqlServerConfigs.DatabaseName,
                TrustServerCertificate = true
            };

            return new SqlConnection(builder.ConnectionString);
        }
    }
}
