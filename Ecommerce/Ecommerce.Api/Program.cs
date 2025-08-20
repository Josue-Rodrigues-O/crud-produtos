using Ecommerce.Api.Middlewares;

namespace Ecommerce.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("secrets/secrets.json", optional: true, reloadOnChange: true);
            builder.ConfigureJwt();
            builder.ConfigureDatabase();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDependencies();
            builder.Services.AddAuthorization();

            var app = builder.Build();
            app.UseMiddleware<ProblemDetailsMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
