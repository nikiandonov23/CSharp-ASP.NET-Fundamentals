using System.ComponentModel.DataAnnotations;

namespace Horizons.Web.ViewModels.Destination;
using static Horizons.GCommon.ValidationConstants;
public class EditDestinationTerrainDropDownViewModel
{
    public int Id { get; set; }



    [Required]
    [MinLength(TerrainNameMinLength)]
    [MaxLength(TerrainNameMaxLength)]
    public string Name { get; set; } = null!;
}