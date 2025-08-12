namespace BookVerse.GCommon
{
    public static class ValidationConstants
    {
        public const int BookTitleMinLength = 3;
        public const int BookTitleMaxLength = 80;

        public const int BookDescriptionMinLength = 10;
        public const int BookDescriptionMaxLength = 250;


        //Използвам този формат на дататата за да ми се пази календарчето в браузъра
        public const string ValidDateFormat = "yyyy-MM-dd";
    

  
        public const int GenreNameMinLength = 3;
        public const int GenreNameMaxLength = 20;

    }
}
