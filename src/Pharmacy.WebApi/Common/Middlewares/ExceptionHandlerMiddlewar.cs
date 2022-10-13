using Newtonsoft.Json;
using Pharmacy.WebApi.Common.Exceptions;

namespace Pharmacy.WebApi.Common.Middlewares
{
    public class ExceptionHandlerMiddlewar
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddlewar(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (StatusCodeException statusCodeException)
            {
                await HandlerAsync(statusCodeException, context);
            }
            catch (Exception exception)
            {
                await HandlerOtherAsync(exception, context);
            }
        }
        public async Task HandlerAsync(StatusCodeException statusCodeException, HttpContext httpContext)
        {
            httpContext.Response.StatusCode = (int)statusCodeException.StatusCode;
            httpContext.Response.ContentType = "application/json";
            string json = JsonConvert.SerializeObject(new { StatusCode = statusCodeException.StatusCode, Message = statusCodeException.Message });
            await httpContext.Response.WriteAsync(json);
        }
        public async Task HandlerOtherAsync(Exception exception, HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "application/json";
            string json = JsonConvert.SerializeObject(new { Message = exception.Message });
            await httpContext.Response.WriteAsync(json);
        }
    }
}
