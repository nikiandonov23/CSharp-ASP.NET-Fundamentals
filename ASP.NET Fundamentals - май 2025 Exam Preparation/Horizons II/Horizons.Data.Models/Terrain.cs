using System.ComponentModel.DataAnnotations;
using static Horizons.GCommon.ValidationConstants;
namespace Horizons.Data.Models;

public class Terrain
{
    [Key]
    public int Id { get; set; }


    [Required]
    [MaxLength(TerrainNameMaxLength)]
    [MinLength(TerrainNameMinLength)]
    public string Name { get; set; } = null!;

    public ICollection<Destination> Destinations { get; set; } = new HashSet<Destination>();


    //	Has Id – a unique integer, Primary Key
    //	Has Name – a string with min length 3 and max length 20 (required)
    //	Has Destinations – a collection of type Destination
}