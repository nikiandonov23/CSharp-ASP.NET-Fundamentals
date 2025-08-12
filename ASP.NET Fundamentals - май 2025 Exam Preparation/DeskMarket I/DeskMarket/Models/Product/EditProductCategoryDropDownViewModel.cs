using System.ComponentModel.DataAnnotations;

namespace DeskMarket.Models.Product;
using static DeskMarket.GComon.ValidationConstants;
public class EditProductCategoryDropDownViewModel
{
    [Required]
    public int Id { get; set; }

    [MinLength(CategoryNameMinLength)]
    [MaxLength(CategoryNameMaxLength)]
    public string Name { get; set; } = null!;
}