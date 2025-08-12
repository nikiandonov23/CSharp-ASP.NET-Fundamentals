using System.ComponentModel.DataAnnotations;
using static BookVerse.GCommon.ValidationConstants;
namespace BookVerse.ViewModels.Book;

public class CreateBookGenreDropDownViewModel
{
    [Required]
    public int Id { get; set; }



    [Required]
    [MinLength(GenreNameMinLength)]
    [MaxLength(GenreNameMaxLength)]
    public string Name { get; set; } = null!;
}