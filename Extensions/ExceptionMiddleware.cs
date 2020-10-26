using Microsoft.AspNetCore.Builder;

namespace Okta_Web.Extensions
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<Middlewares.ExceptionMiddleware>();
        }
    }
}
