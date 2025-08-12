using System.ComponentModel.DataAnnotations;

namespace RecipeSharingPlatform.ViewModels.Recipe;
using static RecipeSharingPlatform.GCommon.ValidationConstants;
public class EditRecipeViewModel
{
    public int Id { get; set; }


    [Required]
    [MinLength(RecipeTitleMinLength)]
    [MaxLength(RecipeTitleMaxLength)]
    public string Title { get; set; } = null!;


    public int CategoryId { get; set; }
    public IEnumerable<EditRecipeCategoriesDropDownViewModel> Categories { get; set; }=new List<EditRecipeCategoriesDropDownViewModel>();



    [Required]
    [MinLength(RecipeInstructionsMinLength)]
    [MaxLength(RecipeInstructionsMaxLength)]
    public string Instructions { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public DateTime CreatedOn { get; set; } 

}