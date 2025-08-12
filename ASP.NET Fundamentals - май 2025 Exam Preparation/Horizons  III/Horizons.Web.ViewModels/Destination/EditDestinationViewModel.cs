using System.ComponentModel.DataAnnotations;

namespace Horizons.Web.ViewModels.Destination;
using static Horizons.GCommon.ValidationConstants;
public class EditDestinationViewModel
{
    public int Id { get; set; }


    [Required]
    [MinLength(DestinationNameMinLength)]
    [MaxLength(DestinationNameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    public DateTime PublishedOn { get; set; } 



    [Required]
    [MinLength(DestinationDescriptionMinLength)]
    [MaxLength(DestinationDescriptionMaxLength)]
    public string Description { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public int TerrainId { get; set; }

    public IEnumerable<EditDestinationTerrainDropDownViewModel> Terrains { get; set; } =
        new List<EditDestinationTerrainDropDownViewModel>();





    public string PublisherId { get; set; } = null!;
}