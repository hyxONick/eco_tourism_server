namespace eco_tourism_outdoor.Models;

public class ProductOrderInfo
{
    public int Id { get; set; } // Primary key, unique identifier for the product order
    public int BuyerId { get; set; } // Foreign key, associated buyer's ID
    public int ProductId { get; set; } // Foreign key, associated product's ID
    public string Memo { get; set; } = string.Empty; // Notes or remarks related to the order
    public decimal TotalAmount { get; set; } // Total price of the order
    public decimal? NextAmount { get; set; } // Discounted total price
    public DateTime CreatedAt { get; set; } // Timestamp when the record was created
    public DateTime UpdatedAt { get; set; } // Timestamp when the record was last updated
    public bool IsDeleted { get; set; } // Indicates whether the record is marked as deleted (true for deleted, false otherwise)
}
