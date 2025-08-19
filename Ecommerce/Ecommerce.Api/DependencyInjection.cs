using Ecommerce.Application.Services;
using Ecommerce.Domain;
using Ecommerce.Infrastructure.Configurations;
using Ecommerce.Infrastructure.MySql;
using Ecommerce.Infrastructure.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ecommerce.Api
{
    public static class DependencyInjection
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, MySqlProductRepository>();
            services.AddScoped<ProductService>();
            services.AddScoped<AuthService>();
        }

        public static void ConfigureJwt(this WebApplicationBuilder builder)
        {
            var jwt = new JwtConfig(builder.Configuration);
            var keyBytes = Encoding.UTF8.GetBytes(jwt.Key);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwt.Issuer,
                        ValidAudience = jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}
