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




    [Required]
    public DateTime CreatedOn { get; set; }

    [Required]
    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;



    [Required]
    public bool IsDeleted { get; set; } = false;

    public ICollection<UserRecipe> UsersRecipes { get; set; } = new HashSet<UserRecipe>();



    //	Has Id – a unique integer, Primary Key
    //	Has Title – a string with min length 3 and max length 80 (required)
    //	Has Instructions – string with min length 10 and max length 250 (required)
    //	Has ImageUrl – nullable string (not required)
    //	Has AuthorId – string (required)
    //	Has Author – IdentityUser(required)
    //   •	Has CreatedOn – DateTime with format "dd-MM-yyyy" (required)
    //   o   The DateTime format is only recommended
    //	Has CategoryId – integer, foreign key(required)
    //   •	Has Category – Category(required)
    //   •	Has IsDeleted – bool (default value == false)
    //	Has UsersRecipes – a collection of type UserRecipe

}