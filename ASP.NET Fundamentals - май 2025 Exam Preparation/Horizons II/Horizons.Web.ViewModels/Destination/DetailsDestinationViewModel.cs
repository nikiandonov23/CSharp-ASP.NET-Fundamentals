using System.ComponentModel.DataAnnotations;

namespace Horizons.Web.ViewModels.Destination;
public class DetailsDestinationViewModel
{
    public string Id { get; set; } = null!;
    public string? ImageUrl { get; set; }

    
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Publisher { get; set; } = null!;
    public string Terrain { get; set; } = null!;
    public DateTime PublishedOn { get; set; }
    public bool IsPublisher { get; set; } = false;
    public bool IsFavorite { get; set; } = false;

    //  ImageUrl
    //      Name
    //  Description
    //      Terrain(only the name of the terrain)
    //  PublishedOn
    //      Publisher(the email of the publisher)
    //  IsPublisher(bool koito pokazva dali lognatiq userId == PublisherId)
    //  IsFavorite(bool koito pokazva dali lognatiq user.Contains any offfff)
    //  Id

}