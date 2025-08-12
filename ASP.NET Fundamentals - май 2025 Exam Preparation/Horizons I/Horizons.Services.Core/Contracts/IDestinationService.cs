using Horizons.Web.ViewModels.Destination;

namespace Horizons.Services.Core.Contracts
{
    public interface IDestinationService
    {
        public Task<IEnumerable<DestinationIndexViewModel>> GetDestinationsIndexAsync(string? userId);

        public Task<DestinationDetailsViewModel?> GetDestinationByIdAsync(int? id,string userId);

        public Task<bool> AddDestinationAsync(string userId,AddDestinationInputModel inputModel);

        public Task<bool> EditDestinationAsync(string userId,EditDestinationViewModel inputModel);

    }
}
