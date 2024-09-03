namespace eco_tourism_tourist.Models;

public class TouristInfo
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Interests { get; set; } // can empty
    public string? Preferences { get; set; } // can empty
    public int Grade { get; set; }
    public int Credit { get; set; }
    public string? Language { get; set; } // can empty
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}

