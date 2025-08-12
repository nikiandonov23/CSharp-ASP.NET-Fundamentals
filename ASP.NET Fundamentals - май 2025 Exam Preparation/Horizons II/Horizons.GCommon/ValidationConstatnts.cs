namespace Horizons.GCommon;

public class ValidationConstants
{
    // Destination
    public const int DestinationNameMinLength = 3;
    public const int DestinationNameMaxLength = 80;
    public const int DestinationDescriptionMinLength = 10;
    public const int DestinationDescriptionMaxLength = 250;
    public const int DestinationImageUrlMaxLength = 2048; // Adjust as needed
    public const string DestinationDateFormat = "dd/MM/yyyy";

    // Terrain
    public const int TerrainNameMinLength = 3;
    public const int TerrainNameMaxLength = 20;
}