namespace eco_tourism_outdoor.Models;

public class ProductInfo
{
    public int Id { get; set; } // Primary key, unique identifier for the product
    public required string Name { get; set; } // Product name
    public string ProductType { get; set; } = string.Empty; // Product type/category
    public string Description { get; set; } = string.Empty; // Product description
    public decimal Price { get; set; } // Price per unit
    public decimal? NextPrice { get; set; } // Discounted price per unit
    public decimal? RentalPrice { get; set; } // Rental price per unit
    public DateTime CreatedAt { get; set; } // Timestamp when the record was created
    public DateTime UpdatedAt { get; set; } // Timestamp when the record was last updated
    public bool IsDeleted { get; set; } // Indicates whether the record is marked as deleted (true for deleted, false otherwise)
}