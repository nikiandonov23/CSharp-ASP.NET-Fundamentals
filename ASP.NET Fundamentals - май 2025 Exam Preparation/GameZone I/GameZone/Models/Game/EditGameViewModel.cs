using System.ComponentModel.DataAnnotations;

namespace GameZone.Models.Game;
using static GameZone.GCommon.ValidationConstants;
public class EditGameViewModel
{
    public int Id { get; set; }

    [Required]
    [MinLength(GameTitleMinLength)]
    [MaxLength(GameTitleMaxLength)]
    public string Title { get; set; } = null!;

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

    [Required]
    public bool IsDeleted { get; set; } = false;


    public IEnumerable<EditGameGenreDropDownViewModel> Genres { get; set; } =
        new List<EditGameGenreDropDownViewModel>();
}