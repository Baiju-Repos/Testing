using Microsoft.AspNetCore.Http;
using Okta_Web.Helpers;
using System;
using System.Threading.Tasks;

namespace Okta_Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await LogWriter.WriteLogFile("Exception occured.", ex);
            }
        }
    }
}
