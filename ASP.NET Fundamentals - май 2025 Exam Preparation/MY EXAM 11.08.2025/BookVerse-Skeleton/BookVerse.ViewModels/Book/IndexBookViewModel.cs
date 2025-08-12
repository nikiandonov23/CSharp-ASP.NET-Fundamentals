namespace BookVerse.ViewModels.Book;

public class IndexBookViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? CoverImageUrl { get; set; }

    public string Genre { get; set; } = null!;

    public int SavedCount { get; set; }


    // За логиката в Razor-а:
    public bool IsAuthor { get; set; }  // дали текущият потребител е автор
    public bool IsSaved { get; set; }   // дали е добавена в "My Books"
}