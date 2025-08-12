using System.ComponentModel.DataAnnotations;
using static Horizons.GCommon.ValidationConstants;
namespace Horizons.Web.ViewModels.Destination;

public class AddDestinationViewModel
{
    [MaxLength(DestinationNameMaxLength)]
    [MinLength(DestinationNameMinLength)]
    public string Name { get; set; } = null!;

    public int TerrainId { get; set; }


    [MaxLength(DestinationDescriptionMaxLength)]
    [MinLength(DestinationDescriptionMinLength)]

    public string Description { get; set; } = null!;


    [MaxLength(DestinationImageUrlMaxLength)]
    public string? ImageUrl { get; set; }




    [DataType(DataType.Date)]
    [Required]
    public DateTime PublishedOn { get; set; } 


    public IEnumerable<AddDestinationTerrainDropDownModel> Terrains { get; set; } =
        new List<AddDestinationTerrainDropDownModel>();

}