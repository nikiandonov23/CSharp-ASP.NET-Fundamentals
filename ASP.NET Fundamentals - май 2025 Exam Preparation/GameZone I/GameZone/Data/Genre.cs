using System.ComponentModel.DataAnnotations;

namespace GameZone.Data;
using static GameZone.GCommon.ValidationConstants;
public class Genre
{
    public int Id { get; set; }


    [Required]
    [MaxLength(GameDescriptionMaxLength)]
    [MinLength(GameDescriptionMinLength)]
    public string Name { get; set; } = null!;
    public ICollection<Game> Games { get; set; } = new List<Game>();
}