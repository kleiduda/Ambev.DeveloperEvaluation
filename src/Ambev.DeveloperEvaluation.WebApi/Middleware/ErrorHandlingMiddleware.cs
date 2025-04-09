using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentValidation;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await WriteErrorAsync(context, 400, "ValidationError", "Invalid input data", ex.Errors.FirstOrDefault()?.ErrorMessage ?? ex.Message);
            }
            catch (DomainException ex)
            {
                await WriteErrorAsync(context, 400, "DomainException", "Business rule violation", ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                await WriteErrorAsync(context, 404, "ResourceNotFound", "Resource not found", ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                await WriteErrorAsync(context, 401, "AuthenticationError", "Invalid authentication token", ex.Message);
            }
            catch (Exception ex)
            {
                await WriteErrorAsync(context, 500, "InternalServerError", "An unexpected error occurred", ex.Message);
            }
        }

        private static Task WriteErrorAsync(HttpContext context, int statusCode, string type, string error, string detail)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new ErrorResponse
            {
                Type = type,
                Error = error,
                Detail = detail
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }

}
