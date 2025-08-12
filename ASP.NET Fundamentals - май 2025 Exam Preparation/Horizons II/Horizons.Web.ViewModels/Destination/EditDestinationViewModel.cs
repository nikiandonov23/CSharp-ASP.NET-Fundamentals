using System.ComponentModel.DataAnnotations;

namespace Horizons.Web.ViewModels.Destination;
using static Horizons.GCommon.ValidationConstants;
public class EditDestinationViewModel
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


    public int Id { get; set; }
    public string PublisherId { get; set; } = null!;
    public ICollection<EditDestinationTerrainDropDownModel> Terrains { get; set; } = new List<EditDestinationTerrainDropDownModel>();
}