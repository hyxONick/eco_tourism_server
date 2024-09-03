namespace eco_tourism_tourist.Models;

public class TouristOrderInfo
{
    public int Id { get; set; }
    public int SceneId { get; set; }
    public int GuestId { get; set; }
    public string? Memo { get; set; } // can empty
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
