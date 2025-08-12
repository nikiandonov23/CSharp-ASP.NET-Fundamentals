using System.ComponentModel.DataAnnotations;

namespace Horizons.Web.ViewModels.Destination;
using static Horizons.GCommon.ValidationConstants;
public class AddDestinationInputModel
{
    //Poneje e input model trqbva da ima validacii 

    [Required]
    [MinLength(DestinationNameMinLength)]
    [MaxLength(DestinationNameMaxLength)]
    public string Name { get; set; } = null!;




    [Required]
    public int TerrainId { get; set; }

    public IEnumerable<AddDestinationTerrainDropDownModel>? Terrains { get; set; } 

    [Required]
    [MinLength(DescriptionMinLength)]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; } = null!;



    public string? ImageUrl { get; set; }

    [Required]
    [MinLength(10)]
    [MaxLength(10)]
    public string PublishedOn { get; set; } = null!;


}