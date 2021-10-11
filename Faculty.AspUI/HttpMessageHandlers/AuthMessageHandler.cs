using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Faculty.AspUI.HttpMessageHandlers
{
    public class AuthMessageHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthMessageHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["access_token"];
            if (string.IsNullOrEmpty(token) == false) request.Headers.Add("Authorization", "Bearer " + token);
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}
