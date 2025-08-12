using System.ComponentModel.DataAnnotations;

namespace RecipeSharingPlatform.ViewModels.Recipe;
using static RecipeSharingPlatform.GCommon.ValidationConstants;
public class IndexRecipeViewModel
{
    [Required]
    public int Id { get; set; }
    public string? ImageUrl { get; set; }


    [Required]
    [MaxLength(RecipeTitleMaxLength)]
    [MinLength(RecipeTitleMinLength)]
    public string Title { get; set; } = null!;


    [Required]
    [MaxLength(CategoryNameMaxLength)]
    [MinLength(CategoryNameMinLength)]
    public string Category { get; set; } = null!;
    public int SavedCount { get; set; }
    public bool IsAuthor { get; set; } = false;
    public bool IsSaved { get; set; } = false;
    

}