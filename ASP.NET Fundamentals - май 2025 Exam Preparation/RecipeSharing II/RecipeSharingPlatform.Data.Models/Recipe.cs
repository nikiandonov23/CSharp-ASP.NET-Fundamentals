using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static RecipeSharingPlatform.GCommon.ValidationConstants;
namespace RecipeSharingPlatform.Data.Models;

public class Recipe
{

    [Key]
    public int Id { get; set; }


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
    [ForeignKey(nameof(Author))]
    public string AuthorId { get; set; } = null!;
    public IdentityUser Author { get; set; } = null!;



    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;




    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedOn { get; set; }





    [Required]
    public bool IsDeleted { get; set; } = false;

    public ICollection<UserRecipe> UsersRecipes { get; set; } = new HashSet<UserRecipe>();
}