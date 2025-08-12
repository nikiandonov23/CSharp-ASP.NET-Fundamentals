using System.ComponentModel.DataAnnotations;

namespace DeskMarket.Models.Product;

public class DetailsProductViewModel
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public string Description { get; set; } = null!;

    public string CategoryName { get; set; } = null!;


    [DataType(DataType.Date)]
    public DateTime AddedOn { get; set; } 

    public string Seller { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public bool HasBought { get; set; }
}