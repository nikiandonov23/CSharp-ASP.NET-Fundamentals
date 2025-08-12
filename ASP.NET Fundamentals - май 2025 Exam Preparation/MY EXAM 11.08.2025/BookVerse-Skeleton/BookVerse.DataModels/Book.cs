namespace BookVerse.DataModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static BookVerse.GCommon.ValidationConstants;
using System.ComponentModel.DataAnnotations.Schema;

public class Book
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(BookTitleMinLength)]
    [MaxLength(BookTitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MinLength(BookDescriptionMinLength)]
    [MaxLength(BookDescriptionMaxLength)]
    public string Description { get; set; } = null!;

    public string? CoverImageUrl { get; set; }



    [Required]
    [ForeignKey(nameof(Publisher))]
    public string PublisherId { get; set; } = null!;
    public IdentityUser Publisher { get; set; } = null!;



    [Required]
    [ForeignKey(nameof(Genre))]
    public int GenreId { get; set; }
    public Genre Genre { get; set; } = null!;




    [Required]
    [DataType(DataType.Date)]
    public DateTime PublishedOn { get; set; }








    [Required]
    public bool IsDeleted { get; set; } = false;

    public ICollection<UserBook> UsersBooks { get; set; } = new HashSet<UserBook>();
}