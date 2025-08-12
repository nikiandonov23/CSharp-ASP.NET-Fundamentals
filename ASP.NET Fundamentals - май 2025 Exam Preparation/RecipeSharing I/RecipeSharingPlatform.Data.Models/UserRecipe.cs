using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace RecipeSharingPlatform.Data.Models;

[PrimaryKey(nameof(UserId), nameof(RecipeId))]
public class UserRecipe
{
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;

    public IdentityUser User { get; set; } = null!;


    [ForeignKey(nameof(Recipe))]
    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; } = null!;
    //	Has UserId – string, PrimaryKey, foreign key(required)
    //   •	Has User – IdentityUser
    //	Has RecipeId – integer, PrimaryKey, foreign key(required)
    //   •	Has Recipe – Recipe

}