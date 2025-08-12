namespace Horizons.Web.ViewModels.Destination;

public class DestinationDetailsViewModel:BaseDestinationViewModel
{
    public string Description { get; set; } = null!;
    public string PublishedOn { get; set; } = null!;
    public string? PublisherName { get; set; } = null!;
}