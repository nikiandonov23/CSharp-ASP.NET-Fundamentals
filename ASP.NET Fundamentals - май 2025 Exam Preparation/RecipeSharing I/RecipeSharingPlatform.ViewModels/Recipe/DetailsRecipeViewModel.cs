namespace RecipeSharingPlatform.ViewModels.Recipe;

public class DetailsRecipeViewModel
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; }
    public string Title { get; set; } = null!;
    public string Instructions { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string CreatedOn { get; set; } = null!;
    public string Author { get; set; } = null!;
    public bool IsAuthor { get; set; }=false;
    public bool IsSaved { get; set; } = false;
    

}