using Microsoft.AspNetCore.Mvc;
using eco_tourism_user.Models;
using System.Threading.Tasks;
using eco_tourism_user.Services;

namespace eco_tourism_user.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserInfoService _userService;

        public UserController(IUserInfoService userInfoService) => 
            _userService = userInfoService;
    
        // POST: User/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // Register the user using the request properties
            var user = await _userService.RegisterAsync(
                request.Username, 
                request.Password, 
                request.Email, 
                request.Role, 
                request.Address, 
                request.Description
            );

            // Check if the user registration was successful
            if (user == null)
            {
                return Conflict("Username already taken."); // Username conflict response
            }

            // Return Created response with the new user's details
            return CreatedAtAction(nameof(Login), new { username = user.Name }, user);
        }

        // POST: User/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.LoginAsync(request.Username, request.Password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok(user);
        }

        // Post: User/rewards/{userId}
        [HttpPost("rewards/{userId}")]
        public async Task<IActionResult> UpdateRewards(int userId, [FromBody] UpdateRewardsRequest request)
        {
            var user = await _userService.UpdateRewardsAsync(userId, request.PointsToAdd);
            if (user == null)
            {
                return NotFound("User not found or is deleted.");
            }
            return Ok(user);
        }

        // GET: User/{userId}/validate-token
        [HttpGet("{userId}/validate-token")]
        public async Task<IActionResult> ValidateToken(int userId, [FromQuery] string token)
        {
            var isValid = await _userService.ValidateTokenAsync(userId, token);
            if (!isValid)
            {
                return Unauthorized("Invalid token.");
            }
            return Ok("Token is valid.");
        }
    }

    // Request models for better structure
    public class RegisterRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public UserRole Role { get; set; }
        public string Address { get; set; } = string.Empty; // User's address
        public string Description { get; set; } = string.Empty; // User's description
    }

    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class UpdateRewardsRequest
    {
        public int PointsToAdd { get; set; }
    }

}
