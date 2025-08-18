using Ecommerce.Application.Services;
using Ecommerce.Domain;
using Ecommerce.Infrastructure.SqlServer;

namespace Ecommerce.Api
{
    public static class DependencyInjection
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ProductService>();
        }
    }
}
