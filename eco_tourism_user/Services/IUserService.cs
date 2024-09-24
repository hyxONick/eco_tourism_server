using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using eco_tourism_user.DB;
using eco_tourism_user.Models;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace eco_tourism_user.Services 
{
    public interface IUserInfoService
    {
        Task<User?> LoginAsync(string username, string password);
        Task<User?> RegisterAsync(string username, string password, string email, UserRole role, string address, string description);
        Task<User?> UpdateRewardsAsync(int userId, int pointsToAdd);
        Task<bool> ValidateTokenAsync(int userId, string token);
    }
    public class UserInfoService : IUserInfoService
    {
        private readonly EcoTourismUserContext _context;

        public UserInfoService(EcoTourismUserContext context)
        {
            _context = context;
        }
        // Login and return user with token
        public async Task<User?> LoginAsync(string username, string password)
        {
            // Find user by username and verify password
            var user = await _context.User.FirstOrDefaultAsync(u => u.Name == username);

            if (user == null || !VerifyPassword(password, user.Password))
                return null;

            // Generate token for the authenticated user
            var token = GenerateToken(user.Name);

            // Store the token in the user model (if needed)
            user.Token = token; 
            await _context.SaveChangesAsync();

            return user; // Return the user with the token
        }

        // Register a new user
        public async Task<User?> RegisterAsync(string username, string password, string email, UserRole role, string address, string description)
        {
            // Check if the username already exists
            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Name == username);
            if (existingUser != null)
            {
                return null; // Username already taken
            }

            // Hash the password before storing
            var hashedPassword = HashPassword(password);

            // Create a new user object
            var newUser = new User
            {
                Name = username,
                Password = hashedPassword,
                Email = email,
                Role = role,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false // Default to not deleted
            };

            // Add and save the new user to the database
            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            // Optionally, generate a token for the new user
            newUser.Token = GenerateToken(newUser.Name);
            await _context.SaveChangesAsync();

            return newUser; // Return the newly created user
        }

        public async Task<User?> UpdateRewardsAsync(int userId, int pointsToAdd)
        {
            var user = await _context.User.FindAsync(userId);
            if (user == null || user.IsDeleted)
            {
                return null; // User not found or is deleted
            }

            // Update the rewards points
            user.Rewards += pointsToAdd; // You can also implement more logic here if needed
            user.UpdatedAt = DateTime.UtcNow; // Update the timestamp
            await _context.SaveChangesAsync();

            return user; // Return the updated user
        }

        // Validate token for a specific user
        public async Task<bool> ValidateTokenAsync(int userId, string token)
        {
            var user = await _context.User.FindAsync(userId);
            return user != null && user.Token == token;
        }

        // Generate JWT token
        private string GenerateToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eco_tourism_nick_andrea_lee_shuang_izzy_secret_key_by_csci927"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, UserRole.User.ToString()) // Set role claim
            };

            var token = new JwtSecurityToken(
                issuer: "eco-tourism.au",
                audience: "eco-tourism",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Verify if the provided password matches the stored hashed password
        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Implement password verification logic (e.g., using BCrypt or other hashing algorithm)
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedPasswordHash);
        }

        // Example method to hash passwords before saving a user
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }

}

