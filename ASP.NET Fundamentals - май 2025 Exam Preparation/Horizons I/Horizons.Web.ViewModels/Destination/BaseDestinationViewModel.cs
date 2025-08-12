namespace Horizons.Web.ViewModels.Destination;

public abstract class BaseDestinationViewModel
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; }
    public string Name { get; set; } = null!;
    public string TerrainName { get; set; } = null!;
    public bool IsPublisher { get; set; }
    public bool IsFavorite { get; set; }
}