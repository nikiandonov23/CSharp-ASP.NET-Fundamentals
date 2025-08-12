namespace DeskMarket.GComon;

public static class ValidationConstants
{
    // Product constants
    public const int ProductNameMinLength = 2;
    public const int ProductNameMaxLength = 60;

    public const int ProductDescriptionMinLength = 10;
    public const int ProductDescriptionMaxLength = 250;



    public const string ProductPriceMinValueString = "1.00";
    public const string ProductPriceMaxValueString = "3000.00";


    // Date format Ползвам тоя щото искам в браузъра да се показва календарче и предварително заредена дата !!!!
    public const string ValidDateFormat = "yyyy-MM-dd";

    // Category constants 
    public const int CategoryNameMinLength = 3;
    public const int CategoryNameMaxLength = 20;
}