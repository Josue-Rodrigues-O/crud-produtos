using Ecommerce.Infrastructure.Configurations;
using Ecommerce.Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Ecommerce.Infrastructure.Scripts.MySql.StartDatabase
{
    public static class MySqlInit
    {
        public static void StartDatabase(IConfiguration configuration)
        {
            var mySql = new MySqlConfig(configuration);
            var script = FilesHelpers.ReadEmbeddedResource("mysql_init.sql");
            script = script.Replace("{{DB_NAME}}", mySql.DatabaseName);
            var connectionString = $"server={mySql.Server};uid={mySql.User};pwd={mySql.Password};database=sys";

            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            using var command = new MySqlCommand(script, connection);
            command.ExecuteNonQuery();
        }
    }
}
