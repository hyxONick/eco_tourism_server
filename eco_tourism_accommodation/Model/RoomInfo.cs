namespace eco_tourism_accommodation.Models;

public class RoomInfo
{
    public int Id { get; set; }

    public string RoomName { get; set; } = string.Empty;

    public string RoomType { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;
    
    public int NumberOfBeds { get; set; }

    public string PicUrl { get; set; } = string.Empty;
    
    public int Capacity { get; set; }
    public string Description { get; set; } = string.Empty;

    public double Price { get; set; }

    public int CreditPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;
}