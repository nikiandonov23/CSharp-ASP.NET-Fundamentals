using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static Horizons.GCommon.ValidationConstants;

namespace Horizons.Data.Models;

public class Destination
{
    [Key]

    public int Id { get; set; }


    [Required]
    [MinLength(DestinationNameMinLength)]
    [MaxLength(DestinationNameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MinLength(DestinationDescriptionMinLength)]
    [MaxLength(DestinationDescriptionMaxLength)]
    public string Description { get; set; } = null!;


    [MaxLength(DestinationImageUrlMaxLength)]
    public string? ImageUrl { get; set; }



    [DisplayFormat(DataFormatString = DestinationDateFormat, ApplyFormatInEditMode = true)]
    public DateTime PublishedOn { get; set; }



    public bool IsDeleted { get; set; } = false;


    //•	Has PublisherId – string (required)
    //•	Has Publisher – IdentityUser(required)
    [ForeignKey(nameof(Publisher))]
    public string PublisherId { get; set; } = null!;
    public IdentityUser Publisher { get; set; } = null!;



    [ForeignKey(nameof(Terrain))]
    public int TerrainId { get; set; }
    public Terrain Terrain { get; set; } = null!;



    public ICollection<UserDestination> UsersDestinations { get; set; } = new HashSet<UserDestination>();

    //•	Has UsersDestinations – a collection of type UserDestination

}