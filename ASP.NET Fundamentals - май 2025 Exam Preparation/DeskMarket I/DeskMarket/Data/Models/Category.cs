using System.ComponentModel.DataAnnotations;
using static DeskMarket.GComon.ValidationConstants;
namespace DeskMarket.Data.Models;

public class Category
{
    [Key]
    public int Id { get; set; }


    [Required]
    [MinLength(CategoryNameMinLength)]
    [MaxLength(CategoryNameMaxLength)]
    public string Name { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = null!;
}