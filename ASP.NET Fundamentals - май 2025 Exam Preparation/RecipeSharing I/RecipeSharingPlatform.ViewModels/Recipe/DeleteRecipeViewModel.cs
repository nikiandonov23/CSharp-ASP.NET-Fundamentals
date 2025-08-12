namespace RecipeSharingPlatform.ViewModels.Recipe;

public class DeleteRecipeViewModel
{
    public string Author { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int Id { get; set; }
    public string AuthorId { get; set; } = null!;

}