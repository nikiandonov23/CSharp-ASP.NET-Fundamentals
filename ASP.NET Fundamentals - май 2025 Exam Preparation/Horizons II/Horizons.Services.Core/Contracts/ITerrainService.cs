using Horizons.Web.ViewModels.Destination;

namespace Horizons.Services.Core.Contracts;

public interface ITerrainService
{
     Task<IEnumerable<AddDestinationTerrainDropDownModel>> GetAllTerrainsAsync();
}