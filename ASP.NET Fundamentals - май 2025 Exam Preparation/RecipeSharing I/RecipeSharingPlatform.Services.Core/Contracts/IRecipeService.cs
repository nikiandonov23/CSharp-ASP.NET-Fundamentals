using RecipeSharingPlatform.ViewModels.Recipe;

namespace RecipeSharingPlatform.Services.Core.Contracts
{
    public interface IRecipeService
    {
        //Index GetAllModels
        public Task<IEnumerable<IndexRecipeViewModel>> GetAllRecipesAsync(string? currentUserId);

        //Create ;
        //No predi tova Nov InterfaceDropDown+ServiceDropDown za DROPDOWN podmodela
        //Nov [HttpGet] koito zarejda DropDownModela
        //public async Task<IActionResult> Create()
        //i vrushta View(GLAVNIQ MODE) sus zareden parametur PODMODELA.Koito se vika on podmodel servisa
        public Task<bool> CreateRecipeAsync(string userId, CreateRecipeViewModel inputModel);


        //GetAllFavorites
        public Task<IEnumerable<FavoritesRecipeViewModel>> GetFavoritesRecipesAsync(string userId);

        //Add Recipe from Favorites
        public Task<bool> AddRecipeToFavoritesAsync(string userId, int recipeId);


        //Remove Recipe from Favorites
        public Task<bool> RemoveRecipeFromFavoritesAsync(string userId, int recipeId);


        //GetDetails
        public Task<DetailsRecipeViewModel?> GetRecipeDetailsAsync(int recipeId,string? userId);

        //GetDeleteViewModel
        public Task<DeleteRecipeViewModel?> GetDeleteRecipeViewModelAsync(int recipeId, string? userId);

        //Delete
        public Task<bool> DeleteRecipeAsync(DeleteRecipeViewModel inputModel, string? userId);

        //Edit  .Tuka pravq Edit s prazen dropdown model
        public Task<EditRecipeViewModel?> GetEditRecipeViewModelAsync(string userId, int recipeId);

        //Edit(SAVE)
        //tuka ve4e persistvam promenite SAVE
        //No predi tova Nov InterfaceDropDown+ServiceDropDown za DROPDOWN podmodela
        //Nov [HttpGet] koito zarejda DropDownModela
        //public async Task<IActionResult> Create()
        //i vrushta View(GLAVNIQ MODE) sus zareden parametur PODMODELA.Koito se vika on podmodel servisa
        public Task<bool> EditRecipeAsync(EditRecipeViewModel inputModel, string userId);
    }


}
