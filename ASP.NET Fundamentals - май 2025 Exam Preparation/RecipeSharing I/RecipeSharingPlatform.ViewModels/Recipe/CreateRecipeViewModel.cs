using System.ComponentModel.DataAnnotations;

namespace RecipeSharingPlatform.ViewModels.Recipe;
using static RecipeSharingPlatform.GCommon.ValidationConstants;
public class CreateRecipeViewModel
{

    [MinLength(RecipeTitleMinLength)]
    [MaxLength(RecipeTitleMaxLength)]
    public string Title { get; set; } = null!;

    public int CategoryId { get; set; }
    public IEnumerable<CreateRecipeCategoryDropDownModel> Categories { get; set; }=new List<CreateRecipeCategoryDropDownModel>();

    [MinLength(RecipeInstructionsMinLength)]
    [MaxLength(RecipeInstructionsMaxLength)]
    public string Instructions { get; set; } = null!;
    public string? ImageUrl { get; set; }


    public string CreatedOn { get; set; } = null!;
}