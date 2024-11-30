using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    // 1. Generate JWT Token
    public async Task<string> GenerateJwtToken(IdentityUser user)
    {
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // 2. Get User Role
    public async Task<string> GetUserRole(IdentityUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return roles.FirstOrDefault() ?? "user"; // Return the first role or a default role
    }

    // 3. Refresh JWT Token
    public string RefreshJwtToken(string currentToken)
    {
        // Implement refresh token logic if needed (e.g., validate and issue a new token)
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(currentToken);

        if (token == null || token.ValidTo < DateTime.UtcNow)
        {
            return null; // Invalid or expired token
        }

        var userName = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(userName))
        {
            return null;
        }

        var user = _userManager.FindByNameAsync(userName).Result;
        if (user == null)
        {
            return null;
        }

        return GenerateJwtToken(user).Result;
    }

    // 4. Get User ID from Token
    public string GetUserIdFromToken(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        return jwtToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
    }

    // 5. Invalidate Token
    public void InvalidateToken(HttpContext context)
    {
        // Add token invalidation logic, such as adding it to a blacklist.
    }
}
