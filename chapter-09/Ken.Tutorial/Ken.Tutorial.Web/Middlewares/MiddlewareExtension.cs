using Microsoft.AspNetCore.Builder;

namespace Ken.Tutorial.Web.Middlewares
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseTokenCheck(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenCheckMiddleware>();
        }
    }
}