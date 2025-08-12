using System.ComponentModel.DataAnnotations;
using static BookVerse.GCommon.ValidationConstants;
namespace BookVerse.ViewModels.Book;

public class CreateBookViewModel
{
    [Required]
    [MinLength(BookTitleMinLength)]
    [MaxLength(BookTitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    public int GenreId { get; set; }

    public IEnumerable<CreateBookGenreDropDownViewModel> Genres { get; set; } = new List<CreateBookGenreDropDownViewModel>();

    [Required]
    [MinLength(BookDescriptionMinLength)]
    [MaxLength(BookDescriptionMaxLength)]
    public string Description { get; set; } = null!;

    
    public string? CoverImageUrl { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime PublishedOn { get; set; }
}