using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Faculty.AspUI.Middleware.Implementation
{
    public class LocalizerMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var culture = context.Request.Query["culture"].ToString();
            if (!string.IsNullOrWhiteSpace(culture))
            {
                context.Items["culture"] = culture;
                var cultureInfo = new CultureInfo(culture);
                context.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureInfo)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                    );
            }
            await _next.Invoke(context);
        }
    }
}
