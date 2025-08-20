using Ecommerce.Application.Services;
using Ecommerce.Domain.Repositories;
using Ecommerce.Infrastructure.Configurations;
using Ecommerce.Infrastructure.MySql;
using Ecommerce.Infrastructure.Scripts.MySql.StartDatabase;
using Ecommerce.Infrastructure.Scripts.SqlServer.StartDatabase;
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
            services.AddScoped<ProductService>();
            services.AddScoped<DepartmentService>();
            services.AddScoped<AuthService>();
        }

        public static void ConfigureDatabase(this WebApplicationBuilder builder)
        {
            var provider = builder.Configuration["Database:Provider"];
            switch (provider)
            {
                case "SqlServer":
                    SqlServerInit.StartDatabase(builder.Configuration);
                    builder.Services.AddScoped<IProductRepository, SqlServerProductRepository>();
                    builder.Services.AddScoped<IDepartmentRepository, SqlServerDepartmentRepository>();
                    break;

                case "MySql":
                    MySqlInit.StartDatabase(builder.Configuration);
                    builder.Services.AddScoped<IProductRepository, MySqlProductRepository>();
                    builder.Services.AddScoped<IDepartmentRepository, MySqlDepartmentRepository>();
                    break;

                default:
                    throw new Exception("Provedor de banco desconhecido");
            }
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
