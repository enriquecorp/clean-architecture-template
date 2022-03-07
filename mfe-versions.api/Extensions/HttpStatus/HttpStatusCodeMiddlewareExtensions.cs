using shared.web.infrastructure.Middlewares;


namespace mfe_versions.api.Extensions.Middlewares
{
    public static class HttpStatusCodeMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpStatusCodeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpStatusCodeMiddleware>();
        }
    }   
}
