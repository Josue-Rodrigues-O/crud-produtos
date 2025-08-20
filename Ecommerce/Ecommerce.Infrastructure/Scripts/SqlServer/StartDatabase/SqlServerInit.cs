using Ecommerce.Infrastructure.Configurations;
using Ecommerce.Infrastructure.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Infrastructure.Scripts.SqlServer.StartDatabase
{
    public static class SqlServerInit
    {
        public static void StartDatabase(IConfiguration configuration)
        {
            var sqlServer = new SqlServerConfig(configuration);
            var script = FilesHelpers.ReadEmbeddedResource("sqlserver_init.sql");
            script = script.Replace("{{DB_NAME}}", sqlServer.DatabaseName);

            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = sqlServer.DataSource,
                UserID = sqlServer.User,
                Password = sqlServer.Password,
                InitialCatalog = "master",
                TrustServerCertificate = true
            };

            using var connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

            var batches = script.Split(["GO"], StringSplitOptions.RemoveEmptyEntries);

            foreach (var batch in batches)
            {
                if (!string.IsNullOrWhiteSpace(batch))
                {
                    using var command = new SqlCommand(batch, connection);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
