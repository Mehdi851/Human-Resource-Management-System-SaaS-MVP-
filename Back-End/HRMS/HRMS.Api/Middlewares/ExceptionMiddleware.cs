using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Models;
using System.Text.Json;

namespace HRMS.Api.Middlewares
{
    public sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleException(context, exception);
            }
        }

        private static async Task HandleException(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType = "application/json";

            ApiResponse<object> response;

            switch (exception)
            {
                case ValidationException validation:

                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    response = new ApiResponse<object>
                    {
                        Success = false,
                        Message = validation.Message,
                        Errors = (List<string>)validation.Errors
                    };

                    break;

                case NotFoundException:

                    context.Response.StatusCode = StatusCodes.Status404NotFound;

                    response = new ApiResponse<object>
                    {
                        Success = false,
                        Message = exception.Message
                    };

                    break;

                case ConflictException:

                    context.Response.StatusCode = StatusCodes.Status409Conflict;

                    response = new ApiResponse<object>
                    {
                        Success = false,
                        Message = exception.Message
                    };

                    break;

                case UnauthorizedException:

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    response = new ApiResponse<object>
                    {
                        Success = false,
                        Message = exception.Message
                    };

                    break;

                default:

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    response = new ApiResponse<object>
                    {
                        Success = false,
                        Message = "An unexpected error occurred."
                    };

                    break;
            }

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
