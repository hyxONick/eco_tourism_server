namespace eco_tourism_outdoor.Models;

public class EquipmentRental
{
    public int Id { get; set; } // Primary key, unique identifier for the equipment rental
    public int RenterId { get; set; } // Foreign key, associated renter's ID
    public int ProductId { get; set; } // Foreign key, associated product's ID
    public required string EquipmentType { get; set; } // Type of equipment rented
    public DateTime RentalDate { get; set; } // Date when the equipment was rented
    public DateTime? ReturnDate { get; set; } // Date when the equipment was returned (nullable)
    public RentalStatus Status { get; set; } // Current rental status (e.g., 'Pending', 'Picked up', 'Returned')
    public DateTime CreatedAt { get; set; } // Timestamp when the record was created
    public DateTime UpdatedAt { get; set; } // Timestamp when the record was last updated
    public bool IsDeleted { get; set; } // Indicates whether the record is marked as deleted (true for deleted, false otherwise)
}

// Enum to represent the rental status
public enum RentalStatus
{
    Pending,   // Equipment has not been picked up yet
    PickedUp,  // Equipment has been picked up by the renter
    Returned   // Equipment has been returned by the renter
}
