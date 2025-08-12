using System.ComponentModel.DataAnnotations;
using static Horizons.GCommon.ValidationConstants;
namespace Horizons.Data.Models;

public class Terrain
{

    public int Id { get; set; }


    [Required]
    [MinLength(TerrainNameMinLength)]
    [MaxLength(TerrainNameMaxLength)]   
    public string Name { get; set; } = null!;



    public ICollection<Destination> Destinations { get; set; } = new List<Destination>();

}