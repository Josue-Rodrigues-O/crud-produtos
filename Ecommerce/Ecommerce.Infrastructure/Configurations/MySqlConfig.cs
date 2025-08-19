using Microsoft.Extensions.Configuration;

namespace Ecommerce.Infrastructure.Configurations
{
    public class MySqlConfig(IConfiguration configuration)
    {
        public string Server => configuration["MySql:Server"] ?? throw new ArgumentException("", nameof(Server));
        public string User => configuration["MySql:User"] ?? throw new ArgumentException("", nameof(User));
        public string Password => configuration["MySql:Password"] ?? throw new ArgumentException("", nameof(Password));
        public string DatabaseName => configuration["MySql:DatabaseName"] ?? throw new ArgumentException("", nameof(DatabaseName));
    }
}
