namespace eco_tourism_accommodation.Modules;

public class RoomBooking
{
    public int Id { get; set; }

    public int GuestId { get; set; }

    public int RoomId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public enum Status
    {
        PROCESSING,
        CONFIRMED,
        CANCELED
    };

    public double TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }
}