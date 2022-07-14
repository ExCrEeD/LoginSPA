using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Security;
using System.Threading.Tasks;

namespace LoginApp.Middleware
{
    public class AutorizacionMiddleware
    {
        private readonly RequestDelegate next;
        const string authorization = "Authorization";
        const string keyAuthorizaction = "BA7P?j5}&X";
        public AutorizacionMiddleware(
            RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string urlPeticion = context.Request.GetDisplayUrl().ToLower();
            if (urlPeticion.Contains("/api/"))
            {
                if (context.Request.Headers.ContainsKey(authorization))
                {
                    string headerValue = context.Request.Headers[authorization];
                    if (headerValue != keyAuthorizaction)
                        throw new SecurityException("No tiene acceso para acceder a este recurso.");
                }
                else
                    throw new SecurityException("No tiene acceso para acceder a este recurso.");
            }
            await next(context);
        }
    }

}
