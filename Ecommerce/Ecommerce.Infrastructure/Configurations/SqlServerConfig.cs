using Microsoft.Extensions.Configuration;

namespace Ecommerce.Infrastructure.Configurations
{
    public class SqlServerConfig(IConfiguration configuration)
    {
        public string DataSource => configuration["SqlServer:DataSource"] ?? throw new ArgumentException("", nameof(DataSource));
        public string User => configuration["SqlServer:User"] ?? throw new ArgumentException("", nameof(User));
        public string Password => configuration["SqlServer:Password"] ?? throw new ArgumentException("", nameof(Password));
        public string DatabaseName => configuration["SqlServer:DatabaseName"] ?? throw new ArgumentException("", nameof(DatabaseName));
    }
}
