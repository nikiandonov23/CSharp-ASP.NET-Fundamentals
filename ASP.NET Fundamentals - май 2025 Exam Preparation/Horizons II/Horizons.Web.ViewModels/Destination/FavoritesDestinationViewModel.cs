namespace Horizons.Web.ViewModels.Destination;

public class FavoritesDestinationViewModel
{
    public int Id { get; set; } 
    public string? ImageUrl { get; set; }
    public string Name { get; set; } = null!;
    public string Terrain { get; set; } = null!;
    
}