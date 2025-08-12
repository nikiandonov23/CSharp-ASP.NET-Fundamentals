namespace BookVerse.DataModels;

using System.ComponentModel.DataAnnotations;
using static BookVerse.GCommon.ValidationConstants;
public class Genre
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(GenreNameMinLength)]
    [MaxLength(GenreNameMaxLength)]
    public string Name { get; set; } = null!;

    
    public ICollection<Book> Books { get; set; } = new HashSet<Book>();
}