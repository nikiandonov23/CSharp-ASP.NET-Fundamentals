using Horizons.Web.ViewModels.Destination;

namespace Horizons.Services.Core.Contracts
{
    public interface IDestinationService
    {
        //Index GetAllModels
        public Task<IEnumerable<IndexDestinationViewModel>> GetAllDestinationsAsync(string? currentUserId);
        //    
        //     //Create ;
        //     //No predi tova Nov InterfaceDropDown+ServiceDropDown za DROPDOWN podmodela
        //     //Nov [HttpGet] koito zarejda DropDownModela
        //     //public async Task<IActionResult> Create()
        //     //i vrushta View(GLAVNIQ MODE) sus zareden parametur PODMODELA.Koito se vika on podmodel servisa
        public Task<bool> AddDestinationAsync(string userId, AddDestinationViewModel inputModel);

        //     //GetAllFavorites
        public Task<IEnumerable<FavoritesDestinationViewModel>> GetFavoritesDestinationsAsync(string userId);
        //    
        //     //Add Recipe to Favorites
        public Task<bool> AddDestinationToFavoritesAsync(string userId, int destinationId);
        //    
        //    
        //     //Remove Recipe from Favorites
        public Task<bool> RemoveDestinationFromFavoritesAsync(string userId, int destinationId);



        //     //GetDetails
        public Task<DetailsDestinationViewModel?> GetDetailsDetailsAsync(int destinationId, string? userId);



        //    
        //     //GetDeleteViewModel
             public Task<DeleteDestinationViewModel?> GetDeleteDestinationViewModelAsync(int destinationId, string? userId);
            
        //     //Delete
             public Task<bool> DeleteDestinationAsync(DeleteDestinationViewModel? inputModel, string? userId);
        
        

        //     //Edit  .Tuka pravq Edit s prazen dropdown model
        public Task<EditDestinationViewModel?> GetEditDestinationViewModelAsync(string userId, int destinationId);
        
        


        //     //Edit(SAVE)
        //     //tuka ve4e persistvam promenite SAVE
        //     //No predi tova Nov InterfaceDropDown+ServiceDropDown za DROPDOWN podmodela
        //     //Nov [HttpGet] koito zarejda DropDownModela
        //     //public async Task<IActionResult> Create()
        //     //i vrushta View(GLAVNIQ MODE) sus zareden parametur PODMODELA.Koito se vika on podmodel servisa
            public Task<bool> EditDestinationAsync(EditDestinationViewModel inputModel, string userId);
    }
}
