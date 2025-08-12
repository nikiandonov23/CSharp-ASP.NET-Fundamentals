using System.ComponentModel.DataAnnotations;
using static GameZone.GCommon.ValidationConstants;
namespace GameZone.Models.Game;

public class DetailsGameViewModel
{
    public int Id { get; set; }


    [Required]
    [MinLength(GameTitleMinLength)]
    [MaxLength(GameTitleMaxLength)]
    public string Title { get; set; } = null!;


    [Required]
    [MinLength(GameDescriptionMinLength)]
    [MaxLength(GameDescriptionMaxLength)]
    public string Description { get; set; } = null!;


    [Required]
    [MinLength(GenreNameMinLength)]
    [MaxLength(GenreNameMaxLength)]
    public string Genre { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    public DateTime ReleasedOn { get; set; }


    [Required]
    public string Publisher { get; set; } = null!;

    [Required]
    public bool IsDeleted = false;
    public string? ImageUrl { get; set; }
}