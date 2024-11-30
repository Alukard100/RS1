using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests.User;
using VideoStreamingPlatform.Commons.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using VideoStreamingPlatform.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace VideoStreamingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;
        private readonly VideoStreamingPlatformContext _dbContext;

        public AuthorizationController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IAuthService authService,
            VideoStreamingPlatformContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _authService = authService;
            _dbContext = dbContext;
        }

        // Register a new user
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
                return BadRequest("User already exists.");

            var newUser = new IdentityUser
            {
                UserName = request.UserName,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
                return StatusCode(500, "An error occurred while creating the user.");

            // Assign default role "user"
            await _userManager.AddToRoleAsync(newUser, "user");

            return Ok(new { Message = "Registration successful." });
        }

        // Login and generate JWT token
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, request.Password)))
                return Unauthorized("Invalid username or password.");

            var token = await _authService.GenerateJwtToken(user);
            var role = await _authService.GetUserRole(user);

            return Ok(new { Token = token, UserId = user.Id, Role = role });
        }

        // Refresh JWT token
        [HttpPost("RefreshToken")]
        [Authorize]
        public IActionResult RefreshToken()
        {
            var currentToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var newToken = _authService.RefreshJwtToken(currentToken);

            if (newToken == null)
                return Unauthorized("Invalid token.");

            return Ok(new { Token = newToken });
        }

        // Change user password
        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userId = _authService.GetUserIdFromToken(HttpContext);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return Unauthorized("User not found.");

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
                return BadRequest("Old password is incorrect or new password is invalid.");

            return Ok("Password changed successfully.");
        }

        // Assign role to a user
        [HttpPost("AssignRole")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return NotFound("User not found.");

            // Retrieve the role name from the UserType table
            var userType = await _dbContext.UserTypes.FirstOrDefaultAsync(u => u.TypeId == request.RoleId);
            if (userType == null)
                return NotFound("Role not found.");

            var roleName = userType.Type;
            if (!await _roleManager.RoleExistsAsync(roleName))
                return NotFound("Role does not exist in the system.");

            // Assign the role to the user
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
                return BadRequest("Failed to assign role.");

            return Ok("Role assigned successfully.");
        }

        // Logout and invalidate token
        [HttpPost("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            _authService.InvalidateToken(HttpContext);
            return Ok("Logout successful.");
        }
    }
}
