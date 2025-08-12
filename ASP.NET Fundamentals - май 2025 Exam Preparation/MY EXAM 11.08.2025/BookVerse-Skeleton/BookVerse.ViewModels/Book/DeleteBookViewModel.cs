using System.ComponentModel.DataAnnotations;

namespace BookVerse.ViewModels.Book;

public class DeleteBookViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Publisher { get; set; } = null!;
}