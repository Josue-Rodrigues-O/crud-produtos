using Microsoft.Data.SqlClient;

namespace Ecommerce.Infrastructure.SqlServer
{
    public static class SqlInstance
    {
        public static SqlConnection Create()
        {
            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = "localhost, 1433",
                UserID = "sa",
                Password = "#Ecommerce123",
                InitialCatalog = "Ecommerce",
                TrustServerCertificate = true
            };

            return new SqlConnection(builder.ConnectionString);
        }
    }
}
