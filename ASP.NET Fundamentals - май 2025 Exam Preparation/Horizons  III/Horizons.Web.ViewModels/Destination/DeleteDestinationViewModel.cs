namespace Horizons.Web.ViewModels.Destination;

public class DeleteDestinationViewModel
{
    public string Name { get; set; } = null!;
    public string Publisher { get; set; } = null!;

    public string PublisherId { get; set; } = null!;
    public int Id { get; set; }
}