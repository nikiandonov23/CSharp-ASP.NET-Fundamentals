using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static GameZone.GCommon.ValidationConstants;
namespace GameZone.Data;

public class Game
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(GameTitleMinLength)]
    [MaxLength(GameTitleMaxLength)]
    public string Title { get; set; } = null!;


    [Required]
    [MinLength(GameDescriptionMinLength)]
    [MaxLength(GameDescriptionMaxLength)]
    public string Description { get; set; } = null!;


    public string? ImageUrl { get; set; }



    [Required]
    [ForeignKey(nameof(Publisher))]
    public string PublisherId { get; set; } = null!;
    public IdentityUser Publisher { get; set; } = null!;



    [Required]
    [DataType(DataType.Date)]
    public DateTime ReleasedOn { get; set; }


    [Required]
    [ForeignKey(nameof(Genre))]
    public int GenreId { get; set; }
    public Genre Genre { get; set; }=null!;


    public bool IsDeleted { get; set; } = false;



    public ICollection<GamerGame> GamersGames { get; set; } = new List<GamerGame>();
}