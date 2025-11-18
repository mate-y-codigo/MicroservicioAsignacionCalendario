namespace MicroservicioAsignacionCalendario.Api.Helpers
{
    public static class TokenHelper
    {
        public static string ExtraerToken(this HttpRequest request)
        {
            string authorizationHeader = request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
                return authorizationHeader.Substring(7);
            return string.Empty;
        }
    }
}
