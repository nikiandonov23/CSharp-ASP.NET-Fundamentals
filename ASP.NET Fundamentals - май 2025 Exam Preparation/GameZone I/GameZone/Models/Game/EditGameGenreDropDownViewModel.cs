using System.ComponentModel.DataAnnotations;

namespace GameZone.Models.Game;
using static GameZone.GCommon.ValidationConstants;
public class EditGameGenreDropDownViewModel
{
    public int Id { get; set; }

    [Required]
    [MinLength(GenreNameMinLength)]
    [MaxLength(GenreNameMaxLength)]
    public string Name { get; set; }    = null!;
}