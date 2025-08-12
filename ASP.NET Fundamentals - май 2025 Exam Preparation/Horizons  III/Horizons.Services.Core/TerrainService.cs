using Horizons.Data;
using Horizons.Services.Core.Contracts;
using Horizons.Web.ViewModels.Destination;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Services.Core;

public class TerrainService(ApplicationDbContext terrainService): ITerrainService
{
    public async Task<IEnumerable<AddDestinationTerrainDropDownViewModel>> GetAllTerrainsForAddAsync()
    {
        var terrainsToReturn =await terrainService
            .Terrains
            .Select(x => new AddDestinationTerrainDropDownViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                


            }).ToListAsync();

        return terrainsToReturn;
    }

    public async Task<IEnumerable<EditDestinationTerrainDropDownViewModel>> GetAllTerrainsForEditAsync()
    {
        var allTerrains=await terrainService.Terrains
            .Select(x=>new EditDestinationTerrainDropDownViewModel()
            {

                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();

        return allTerrains;
    }
}