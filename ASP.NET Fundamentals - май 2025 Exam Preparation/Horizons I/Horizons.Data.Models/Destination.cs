using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime;

namespace Horizons.Data.Models;

public class Destination
{
    //	Has Id – a unique integer, Primary Key

    [Key]
    public int Id { get; set; }



    //	Has Name – a string with min length 3 and max length 80 (required)
    [MinLength(3)]
    [MaxLength(80)]
    [Required]
    public string Name { get; set; } = null!;



    //	Has Description – string with min length 10 and max length 250 (required)
    [Required]
    [MinLength(10)]
    [MaxLength(250)]
    public string Description { get; set; } = null!;



    //	Has ImageUrl – nullable string (not required)
    public string? ImageUrl { get; set; }



    //	Has PublisherId – string (required)

    [Required]
    public string PublisherId { get; set; } = null!;



    //	Has Publisher – IdentityUser(required)
    public virtual IdentityUser Publisher { get; set; } = null!;




    //   •	Has PublishedOn – DateTime with format "dd-MM-yyyy" (required)
    //   o   The DateTime format is only recommended
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
    public DateTime PublishedOn { get; set; }





    //	Has TerrainId – integer, foreign key(required)
    [Required]
    [ForeignKey(nameof(Terrain))]
    public int TerrainId { get; set; }



    //   •	Has Terrain – Terrain(required)
    [Required]
    public virtual Terrain Terrain { get; set; } = null!;




    //   •	Has IsDeleted – bool (default value == false)
    public bool IsDeleted { get; set; } = false;




    //	Has UsersDestinations – a collection of type UserDestination
    public virtual ICollection<UserDestination> UsersDestinations { get; set; } = new HashSet<UserDestination>();

}