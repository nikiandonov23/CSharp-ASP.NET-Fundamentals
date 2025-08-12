using System.ComponentModel.DataAnnotations;
using RecipeSharingPlatform.GCommon;

namespace RecipeSharingPlatform.Data.Models;

public class Category
{
    //	Has Id – a unique integer, Primary Key
    //	Has Name – a string with min length 3 and max length 20 (required)
    //	Has Recipes – a collection of type Recipe
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(ValidationConstants.CategoryNameMinLength)]
    [MaxLength(ValidationConstants.CategoryNameMaxLength)]
    public string Name { get; set; } = null!;
    public ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();

}