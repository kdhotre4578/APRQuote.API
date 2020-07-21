using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace APRQuote.API.Extension
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(
                    async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        context.Response.ContentType = "application/json";
                    });
            });
        }
    }
}
