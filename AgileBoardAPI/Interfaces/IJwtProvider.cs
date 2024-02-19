using System.Security.Claims;

namespace AgileBoardAPI.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateJwtToken(ClaimsIdentity claimsIdentity);
        bool IsTokenValid(string token);
        string GetUsernameFromToken(string token);
    }
}
