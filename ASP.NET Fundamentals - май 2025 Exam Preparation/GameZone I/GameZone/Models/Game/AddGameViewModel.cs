using System.ComponentModel.DataAnnotations;
using static GameZone.GCommon.ValidationConstants;
namespace GameZone.Models.Game;

public class AddGameViewModel
{
    [Required]
    [MinLength(GameTitleMinLength)]
    [MaxLength(GameTitleMaxLength)]
    public string Title { get; set; } = null!;

    [StringLength(2048)]
    public string? ImageUrl { get; set; }

    [Required]
    [MinLength(GameDescriptionMinLength)]
    [MaxLength(GameDescriptionMaxLength)]
    public string Description { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    public DateTime ReleasedOn { get; set; }

    


    [Required]
    public int GenreId { get; set; }

    public IEnumerable<AddGameGenreDropDownVIewModel> Genres { get; set; } = new List<AddGameGenreDropDownVIewModel>();
}