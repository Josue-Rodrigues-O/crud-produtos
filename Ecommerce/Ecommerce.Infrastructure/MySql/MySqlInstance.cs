using Ecommerce.Infrastructure.Configurations;
using MySql.Data.MySqlClient;

namespace Ecommerce.Infrastructure.MySql
{
    public static class MySqlInstance
    {
        public static MySqlConnection Create(MySqlConfig mySqlConfigs)
        {
            var connectionString = $"server={mySqlConfigs.Server};uid={mySqlConfigs.User};pwd={mySqlConfigs.Password};database={mySqlConfigs.DatabaseName}";
            return new MySqlConnection(connectionString);
        }
    }
}
