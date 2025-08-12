using System.ComponentModel.DataAnnotations;

namespace Horizons.Data.Models;

public class Terrain
{
    //	Has Id – a unique integer, Primary Key

    [Key]
    public int Id { get; set; }



    //	Has Name – a string with min length 3 and max length 20 (required)
    [MaxLength(20)]
    [MinLength(3)]
    [Required]
    public string Name { get; set; } = null!;





    //	Has Destinations – a collection of type Destination
    public virtual ICollection<Destination> Destinations { get; set; } = new HashSet<Destination>();

}