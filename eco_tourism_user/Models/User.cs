namespace eco_tourism_user.Models
{
    public class User
    {
        public int Id { get; set; } // Unique identifier for the user
        public string Name { get; set; } = string.Empty; // User's name
        public string Address { get; set; } = string.Empty; // User's address
        public int Rewards { get; set; } = 0; // User's Rewards
        public string? Description { get; set; } // Optional description of the user
        public UserRole Role { get; set; } // User role (e.g., Admin or Regular User)
        public string Password { get; set; } = string.Empty; // User's password (hashed)
        public string Email { get; set; } = string.Empty; // User's email address
        public string Token { get; set; } = string.Empty; // Authentication token for the user
        public DateTime CreatedAt { get; set; } // Timestamp of when the user was created
        public DateTime UpdatedAt { get; set; } // Timestamp of the last update
        public bool IsDeleted { get; set; } // Indicates if the user is deleted (soft delete)
    }
    public enum UserRole
    {
        Admin,  // Administrator role
        User    // Regular user role
    }
}
