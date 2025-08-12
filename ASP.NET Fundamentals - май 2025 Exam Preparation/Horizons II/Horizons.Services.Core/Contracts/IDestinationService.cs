using Horizons.Web.ViewModels.Destination;

namespace Horizons.Services.Core.Contracts
{
    public interface IDestinationService
    {
        //IndexAll
        public Task<IEnumerable<IndexDestinationViewModel>> GetAllDestinationsAsync(string? currentUserId);



        //Add 
        public Task<bool> AddDestinationAsync(string currentUserId, AddDestinationViewModel inputModel);




        // Details
        public Task<DetailsDestinationViewModel?> GetDestinationDetailsAsync(int destinationId, string? currentUserId);




        //Get All Favorites 
        public Task<IEnumerable<FavoritesDestinationViewModel>> GetFavoritesDestinationsAsync(string userId);



        //Add  to Favorites
        public Task<bool> AddDestinationToFavoritesAsync(string userId, int destinationId);



        //Remove  from Favorites
        public Task<bool> RemoveDestinationFromFavoritesAsync(string userId, int destinationId);


        //GetForEdit  .Tuka pavq viewModel s prazen dropDownPodmodel
        public Task<EditDestinationViewModel?> GetEditDestinationViewModel(string userId, int destinationId);


        //Edit
        public Task<bool> EditDestinationAsync(EditDestinationViewModel inputModel, string userId);


        //GetForDelete 
        public Task<DeleteDestinationViewModel?> GetDeleteDestinationViewModel(string userId, int destinationId);

        //Delete (SoftDelete)
        public Task<bool> DeleteDestinationAsync(string userId, DeleteDestinationViewModel inputModel);


    }
}
