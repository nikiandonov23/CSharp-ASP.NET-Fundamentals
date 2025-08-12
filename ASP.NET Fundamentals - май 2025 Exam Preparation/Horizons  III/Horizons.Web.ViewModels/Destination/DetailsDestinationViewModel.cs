using System.ComponentModel.DataAnnotations;

namespace Horizons.Web.ViewModels.Destination;
public class DetailsDestinationViewModel
{
    //tuka ne validiram shtroto gi vzemam gotovi
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string Terrain { get; set; } = null!;


    [Required]
    [DataType(DataType.Date)]
    public DateTime PublishedOn { get; set; } 


    public string Publisher { get; set; } = null!;

    public bool IsPublisher { get; set; } = false;

    public bool IsFavorite { get; set; } = false;
}