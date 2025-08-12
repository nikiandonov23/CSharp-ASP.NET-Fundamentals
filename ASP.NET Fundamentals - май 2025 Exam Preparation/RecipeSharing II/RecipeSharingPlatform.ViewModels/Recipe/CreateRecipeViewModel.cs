using System.ComponentModel.DataAnnotations;
using static RecipeSharingPlatform.GCommon.ValidationConstants;
namespace RecipeSharingPlatform.ViewModels.Recipe;

public class CreateRecipeViewModel
{
    [Required]
    [MinLength(RecipeTitleMinLength)]
    [MaxLength(RecipeTitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MinLength(RecipeInstructionsMinLength)]
    [MaxLength(RecipeInstructionsMaxLength)]
    public string Instructions { get; set; } = null!;

    public string? ImageUrl { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public IEnumerable<CreateRecipeCategoryDropDownViewModel> Categories { get; set; } = new List<CreateRecipeCategoryDropDownViewModel>();


    [Required]
    [DataType(DataType.Date)]
    public DateTime CreatedOn { get; set; } 
}