using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static Horizons.GCommon.ValidationConstants;

namespace Horizons.Data.Models;

public class Destination
{
    public int Id { get; set; }

    [Required]
    [MinLength(DestinationNameMinLength)]
    [MaxLength(DestinationNameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MinLength(DestinationDescriptionMinLength)]
    [MaxLength(DestinationDescriptionMaxLength)]
    public string Description { get; set; } = null!;

    public string? ImageUrl { get; set; }

    [Required]
    [ForeignKey(nameof(Publisher))]
    public string PublisherId { get; set; } = null!;

 
    public IdentityUser Publisher { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Terrain))]
    public int TerrainId { get; set; }

   
    public Terrain Terrain { get; set; } = null!;


    [Required]
    public DateTime PublishedOn { get; set; }

    [Required]
    public bool IsDeleted { get; set; } = false;

    public ICollection<UserDestination> UsersDestinations { get; set; } = new List<UserDestination>();
}