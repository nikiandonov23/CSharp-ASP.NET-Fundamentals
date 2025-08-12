using System.ComponentModel.DataAnnotations;

namespace RecipeSharingPlatform.Data.Models;
using static RecipeSharingPlatform.GCommon.ValidationConstants;
public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(CategoryNameMinLength)]
    [MaxLength(CategoryNameMaxLength)]  
    public string Name { get; set; } = null!;


    public ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();
}