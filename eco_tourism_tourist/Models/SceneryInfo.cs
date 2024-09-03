namespace eco_tourism_tourist.Models;

public class SceneryInfo
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Description { get; set; } // can empty
    public string Type { get; set; } = string.Empty;
    public string PicUrl { get; set; } = string.Empty;
    public string? Longitude { get; set; } // can empty
    public string? Latitude { get; set; } // can empty
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
