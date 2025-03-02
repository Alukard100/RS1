using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public interface IAuthService
{
    Task<string> GenerateJwtToken(IdentityUser user);
    Task<string> GetUserRole(IdentityUser user);
    string RefreshJwtToken(string currentToken);
    string GetUserIdFromToken(HttpContext context);
    void InvalidateToken(HttpContext context);
}
