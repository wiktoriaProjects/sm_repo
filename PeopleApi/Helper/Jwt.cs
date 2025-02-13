using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PeopleApi.Helper
{
    public class Jwt
    {
        public static string GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
    }
}
