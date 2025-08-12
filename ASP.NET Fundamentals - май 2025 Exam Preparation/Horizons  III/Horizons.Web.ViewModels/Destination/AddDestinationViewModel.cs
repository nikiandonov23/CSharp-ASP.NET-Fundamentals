using System.ComponentModel.DataAnnotations;

namespace Horizons.Web.ViewModels.Destination;
using static Horizons.GCommon.ValidationConstants;
public class AddDestinationViewModel
{
    [Required]
    [MinLength(DestinationNameMinLength)]
    [MaxLength(DestinationNameMaxLength)]
    public string Name { get; set; } = null!;
    public int TerrainId { get; set; }

    public IEnumerable<AddDestinationTerrainDropDownViewModel> Terrains { get; set; } =
        new List<AddDestinationTerrainDropDownViewModel>();


    [Required]
    [MinLength(DestinationDescriptionMinLength)]
    [MaxLength(DestinationDescriptionMaxLength)]
    public string Description { get; set; } = null!;
    public string? ImageUrl { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime PublishedOn { get; set; } 
}