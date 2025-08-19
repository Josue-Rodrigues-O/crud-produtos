using Microsoft.Extensions.Configuration;

namespace Ecommerce.Infrastructure.Configurations
{
    public class Secrets
    {
        public class Jwt(IConfiguration configuration)
        {
            public string Issuer => configuration["Jwt:Issuer"] ?? throw new ArgumentException("", nameof(Issuer));
            public string Audience => configuration["Jwt:Audience"] ?? throw new ArgumentException("", nameof(Audience));
            public string Key => configuration["Jwt:Key"] ?? throw new ArgumentException("", nameof(Key));
            public int ExpiresMinutes => int.Parse(configuration["Jwt:ExpiresMinutes"] ?? throw new ArgumentException("", nameof(ExpiresMinutes)));
        };

        public record Sql();
    }

}
