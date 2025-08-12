namespace Horizons.Web.ViewModels.Destination;

public class FavoritesDestinationViewModel
{

    //tuka nqma da validiram shtoto gi vzemam gotovi
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string Terrain { get; set; } = null!;
}
