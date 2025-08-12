using DeskMarket.GComon;
using System.ComponentModel.DataAnnotations;
using static DeskMarket.GComon.ValidationConstants;

namespace DeskMarket.Models.Product;

public class EditProductViewModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MinLength(ProductNameMinLength)]
    [MaxLength(ProductNameMaxLength)]
    public string ProductName { get; set; } = null!;

    [Required]
    [Range(typeof(decimal), ProductPriceMinValueString, ProductPriceMaxValueString,
        ErrorMessage = "Price must be between {1} and {2}.")]
    public decimal Price { get; set; }

    [Required]
    [MinLength(ProductDescriptionMinLength)]
    [MaxLength(ProductDescriptionMaxLength)]
    public string Description { get; set; } = null!;

    public string? ImageUrl { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime AddedOn { get; set; }

    [Required]
    public int CategoryId { get; set; }


    [Required]
    public string SellerId { get; set; } = null!;

    public IEnumerable<EditProductCategoryDropDownViewModel> Categories { get; set; } = new List<EditProductCategoryDropDownViewModel>();
}