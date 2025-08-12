using System.ComponentModel.DataAnnotations;

namespace BookVerse.ViewModels.Book;
using static BookVerse.GCommon.ValidationConstants;

public class EditBookGenreDropDownViewModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MinLength(GenreNameMinLength)]
    [MaxLength(GenreNameMaxLength)]
    public string Name { get; set; } = null!;
}