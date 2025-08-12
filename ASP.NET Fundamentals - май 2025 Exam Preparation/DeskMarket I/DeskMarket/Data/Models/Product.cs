using DeskMarket.GComon;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DeskMarket.GComon.ValidationConstants;

namespace DeskMarket.Data.Models;

public class Product
{
    [Key]
    public int Id { get; set; }


    [Required]
    [MinLength(ProductNameMinLength)]
    [MaxLength(ProductNameMaxLength)]
    public string ProductName { get; set; } = null!;

    [Required]
    [MinLength(ProductDescriptionMinLength)]
    [MaxLength(ProductDescriptionMaxLength)]
    public string Description { get; set; } = null!;

    [Required]
    [Range(typeof(decimal), ProductPriceMinValueString, ProductPriceMaxValueString)]
    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    [ForeignKey(nameof(Seller))]
    public string SellerId { get; set; } = null!;

    public IdentityUser Seller { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    public DateTime AddedOn { get; set; }


    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public bool IsDeleted { get; set; } = false;

    public ICollection<ProductClient> ProductsClients { get; set; } = new HashSet<ProductClient>();
}