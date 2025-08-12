using System.ComponentModel.DataAnnotations;

namespace BookVerse.ViewModels.Book;

public class DetailsBookViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? CoverImageUrl { get; set; }

    public string Description { get; set; } = null!;

    public string Genre { get; set; } = null!;


    [DataType(DataType.Date)]
    public DateTime PublishedOn { get; set; }

    public string Publisher { get; set; } = null!;

    public bool IsAuthor { get; set; }

    public bool IsSaved { get; set; }
}