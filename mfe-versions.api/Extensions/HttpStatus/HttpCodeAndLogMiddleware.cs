using shared.web.infrastructure.Middlewares;
using System;



namespace VersioningService.Middlewares
{
    public static class HttpStatusCodeExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpStatusCodeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpStatusCodeMiddleware>();
        }
    }

    
}
