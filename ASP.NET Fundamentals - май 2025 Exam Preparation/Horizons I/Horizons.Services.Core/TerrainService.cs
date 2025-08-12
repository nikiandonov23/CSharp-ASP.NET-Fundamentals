using Horizons.Data;
using Horizons.Services.Core.Contracts;
using Horizons.Web.ViewModels.Destination;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Services.Core;

public class TerrainService(ApplicationDbContext context):ITerrainService
{
    public async Task<IEnumerable<AddDestinationTerrainDropDownModel>> GetTerrainsDropDownModelAsync()
    {
        IEnumerable<AddDestinationTerrainDropDownModel> terrains = await context
            .Terrains
            .AsNoTracking()
            .Select(x => new AddDestinationTerrainDropDownModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();

        return terrains;
    }
}