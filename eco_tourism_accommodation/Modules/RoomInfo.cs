namespace eco_tourism_accommodation.Modules;

public class RoomInfo
{
    public int Id { get; set; }

    public string RoomName { get; set; } = string.Empty;

    public string RoomType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public double Price { get; set; }

    public int CreditPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }
}