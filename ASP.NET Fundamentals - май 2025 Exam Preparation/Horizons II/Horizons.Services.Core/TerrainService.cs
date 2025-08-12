using Horizons.Data;
using Horizons.Services.Core.Contracts;
using Horizons.Web.ViewModels.Destination;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Services.Core;

public class TerrainService(ApplicationDbContext context):ITerrainService
{
    public async Task<IEnumerable<AddDestinationTerrainDropDownModel>> GetAllTerrainsAsync()
    {
        var terrains = await context.Terrains
            .Select(x => new AddDestinationTerrainDropDownModel()
            {

                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

        return terrains;
    }
}