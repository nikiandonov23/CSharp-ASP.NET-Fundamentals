using System.ComponentModel.DataAnnotations;
using static RecipeSharingPlatform.GCommon.ValidationConstants;

namespace RecipeSharingPlatform.ViewModels.Recipe;
public class CreateRecipeCategoryDropDownViewModel
{

    [Required]
    public int Id { get; set; }



    [Required]
    [MinLength(CategoryNameMinLength)]
    [MaxLength(CategoryNameMaxLength)]
    public string Name { get; set; } = null!;
}