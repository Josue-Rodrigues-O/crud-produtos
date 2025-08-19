using Ecommerce.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Ecommerce.Api.Middlewares
{
    public class ProblemDetailsMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                var problem = new ValidationProblemDetails(ex.Errors)
                {
                    Type = "https://httpstatuses.io/400",
                    Title = "Erro de validação",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Um ou mais campos estão inválidos.",
                    Instance = context.Request.Path
                };

                await ProcessException(context, problem);
            }
            catch (Exception ex)
            {
                var problem = new ProblemDetails
                {
                    Type = "https://httpstatuses.io/500",
                    Title = "Erro interno no servidor",
                    Detail = ex.Message,
                    Status = (int)HttpStatusCode.InternalServerError,
                    Instance = context.Request.Path
                };

                await ProcessException(context, problem);
            }
        }

        private static Task ProcessException(HttpContext context, ProblemDetails problem)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problem.Status ?? 500;

            var json = JsonSerializer.Serialize(
                problem,
                problem.GetType(),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return context.Response.WriteAsync(json);
        }
    }
}
