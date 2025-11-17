using System.Net.Http.Headers;

namespace MicroservicioAsignacionCalendario.Api.Auth
{
    public class TokenPropagationHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenPropagationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext?
                .Request.Headers["Authorization"].FirstOrDefault();
            if (token != null)
            {
                if (AuthenticationHeaderValue.TryParse(token, out var headerValue))
                    request.Headers.Authorization = headerValue;
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
