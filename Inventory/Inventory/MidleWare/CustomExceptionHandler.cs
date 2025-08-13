using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Inventory.MidleWare
{
    public static class CustomExceptionHandlerMiddleware
    {
        
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandler>();
        }
    }
    public class CustomExceptionHandler
    {
        private RequestDelegate _handler;

        public CustomExceptionHandler(RequestDelegate handler) => _handler = handler;
        
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _handler(context);
            } catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new
            {
                error = ex.InnerException == null
                    ? ex.Message + ex.StackTrace
                    : ex.InnerException.Message + ex.InnerException.StackTrace
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
