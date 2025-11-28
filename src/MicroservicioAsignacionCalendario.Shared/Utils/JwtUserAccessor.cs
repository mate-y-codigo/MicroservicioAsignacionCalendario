
/*
using System.IdentityModel.Tokens.Jwt;

namespace MicroservicioAsignacionCalendario.Application.Utils
{
    public static class JwtUserAccessor
    {
        public static Guid GetUserId(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new Exception("Token vacío.");

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var sub = jwt.Claims.FirstOrDefault(c =>
                c.Type == JwtRegisteredClaimNames.Sub ||
                c.Type == "sub")?.Value;

            if (string.IsNullOrEmpty(sub))
                throw new Exception("El token no contiene el claim sub.");

            return Guid.Parse(sub);
        }
    }
}
*/