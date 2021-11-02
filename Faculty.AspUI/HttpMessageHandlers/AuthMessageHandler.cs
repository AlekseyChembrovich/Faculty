using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Faculty.AspUI.HttpMessageHandlers
{
    /// <summary>
    /// Authentication message handler for adding bearer headers with jwt token.
    /// </summary>
    public class AuthMessageHandler : DelegatingHandler
    {
        /// <summary>
        /// Http context accessor.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor for init http context accessor.
        /// </summary>
        /// <param name="httpContextAccessor">Http context accessor set up through dependency injection.</param>
        public AuthMessageHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Method for adding in Authorization header bearer and jwt token. 
        /// </summary>
        /// <param name="request">Http request message.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["access_token"];
            if (string.IsNullOrEmpty(token) == false) request.Headers.Add("Authorization", "Bearer " + token);
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}
