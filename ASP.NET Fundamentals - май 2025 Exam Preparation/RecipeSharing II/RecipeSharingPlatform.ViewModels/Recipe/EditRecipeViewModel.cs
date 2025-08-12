using System.ComponentModel.DataAnnotations;
using static RecipeSharingPlatform.GCommon.ValidationConstants;
namespace RecipeSharingPlatform.ViewModels.Recipe;

public class EditRecipeViewModel
{

    [Required]
    public int Id { get; set; }

    [Required]
    [MinLength(RecipeTitleMinLength)]
    [MaxLength(RecipeTitleMaxLength)]
    public string Title { get; set; } = null!;


    [Required]
    public int CategoryId { get; set; }

    public IEnumerable<EditRecipeCategoriesDropDownViewModel> Categories { get; set; } = new List<EditRecipeCategoriesDropDownViewModel>();


    [Required]
    [MinLength(RecipeInstructionsMinLength)]
    [MaxLength(RecipeInstructionsMaxLength)]
    public string Instructions { get; set; } = null!;

    public string? ImageUrl { get; set; }



    [Required]
    [DataType(DataType.Date)]
    public DateTime CreatedOn { get; set; }
}