using System.ComponentModel.DataAnnotations;
using static Horizons.GCommon.ValidationConstants;
namespace Horizons.Web.ViewModels.Destination;

public class AddDestinationTerrainDropDownViewModel
{
    public int Id { get; set; }

    [MinLength(TerrainNameMinLength)]
    [MaxLength(TerrainNameMaxLength)]
    public string Name { get; set; } = null!;
}