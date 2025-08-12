using System.ComponentModel.DataAnnotations;
using static Horizons.GCommon.ValidationConstants;
namespace Horizons.Web.ViewModels.Destination;

public class IndexDestinationViewModel
{
    public int Id { get; set; }

    [Required]
    [MinLength(DestinationNameMinLength)]
    [MaxLength(DestinationNameMaxLength)]
    public string Name { get; set; } = null!;

    public string? ImageUrl { get; set; }

    [Required]
    [MinLength(TerrainNameMinLength)]
    [MaxLength(TerrainNameMaxLength)]
    public string Terrain { get; set; } = null!;

    public int FavoritesCount { get; set; }
    public bool IsPublisher { get; set; } = false;
    public bool IsFavorite { get; set; } = false;
}