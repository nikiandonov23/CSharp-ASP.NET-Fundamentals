namespace DeskMarket.Models.Product;

public class CartProductsViewModel
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }
}