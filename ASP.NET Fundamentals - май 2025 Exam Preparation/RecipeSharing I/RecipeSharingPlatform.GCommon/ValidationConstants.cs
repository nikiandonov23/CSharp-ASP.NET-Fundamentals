namespace RecipeSharingPlatform.GCommon
{
    public static class ValidationConstants
    {

        //Recipe Validation Constants
        public const int RecipeTitleMinLength = 3;
        public const int RecipeTitleMaxLength = 80;
        public const int RecipeInstructionsMinLength = 10;
        public const int RecipeInstructionsMaxLength = 250;

        public const string RecipeCreatedOnFormat = "yyyy-MM-dd";

        //Category Validation Constants
        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 20;

        
    }
}
