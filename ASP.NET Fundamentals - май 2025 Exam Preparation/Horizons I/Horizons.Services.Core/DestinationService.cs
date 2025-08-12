using Horizons.Data;
using Horizons.Data.Models;
using Horizons.Services.Core.Contracts;
using Horizons.Web.ViewModels.Destination;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

namespace Horizons.Services.Core;

public class DestinationService(ApplicationDbContext context) : IDestinationService
{

    //polu4avam userId (az si go vzeh ot baseController-a)
    public async Task<IEnumerable<DestinationIndexViewModel>> GetDestinationsIndexAsync(string? userId)
    {


        IEnumerable<DestinationIndexViewModel> allDestinations = await context
            .Destinations
            .Include(x => x.Terrain)
            .Include(x => x.UsersDestinations)
            .Where(x => x.IsDeleted == false)
            .Select(x => new DestinationIndexViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                TerrainName = x.Terrain.Name,
                FavoritesCount = x.UsersDestinations.Count,
                IsPublisher = userId != null && x.PublisherId == userId,
                IsFavorite = userId != null && x.UsersDestinations.Any(ud => ud.UserId == userId)
            }).ToListAsync();
        return allDestinations;
    }

    //Polu4avam int id (id-to na destinaciqta ot uslovieto) i string userId (az si go zeh ot baseController-a)
    public async Task<DestinationDetailsViewModel?> GetDestinationByIdAsync(int? id, string? userId)
    {
        DestinationDetailsViewModel? detailsViewModel = null;

        if (id.HasValue)
        {
            Destination? destinationEntity = await context.Destinations
                .Include(x => x.Publisher)
                .Include(x => x.Terrain)
                .Include(x => x.UsersDestinations)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id.Value);


            if (destinationEntity != null)
            {
                detailsViewModel = new DestinationDetailsViewModel()
                {
                    Id = destinationEntity.Id,
                    Name = destinationEntity.Name,
                    ImageUrl = destinationEntity.ImageUrl,
                    TerrainName = destinationEntity.Terrain.Name,
                    Description = destinationEntity.Description,
                    PublishedOn = destinationEntity.PublishedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    PublisherName = destinationEntity.Publisher.UserName,



                    //Proverqvam dali userId-to s koeto sum lognat go e sushtoto kato PublisherId v tablicata Destination
                    //Ako e sushtoto kato tova na lognatiq user , zna4i usera e publisher
                    IsPublisher = userId != null ?
                        destinationEntity.PublisherId.ToLower() == userId.ToLower() : false,

                    //proverqvam dali userId-to s koeto sum lognat go ima v tablicata UserDestination
                    //Ako da tova zna4i 4e destinaciqta e vuv favourites na lognatiq user
                    IsFavorite = userId != null ?
                        destinationEntity.UsersDestinations.Any(x => x.UserId.ToLower() == userId.ToLower()) : false

                };
            }


        }

        return detailsViewModel;

    }

    public async Task<bool> AddDestinationAsync(string userId, AddDestinationInputModel inputModel)
    {

        bool isUserValid = await context.Users.AnyAsync(x => x.Id == userId);
        bool isTerrainValid = await context.Terrains.AnyAsync(x => x.Id == inputModel.TerrainId);
        bool isPublishedOnValid = DateTime.TryParseExact(
            inputModel.PublishedOn,
            "dd/MM/yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime publishedOn);
        if (isTerrainValid && isUserValid && isPublishedOnValid)
        {
            var destinationEntity = new Destination()
            {
                Name = inputModel.Name,
                ImageUrl = inputModel.ImageUrl,
                TerrainId = inputModel.TerrainId,
                Description = inputModel.Description,
                PublishedOn = publishedOn,
                PublisherId = userId
            };

            await context.Destinations.AddAsync(destinationEntity);
            await context.SaveChangesAsync();

            return true;
        }

        return false;


    }

    public async Task<bool> EditDestinationAsync(string userId, EditDestinationViewModel inputModel)
    {
        var user = await context.Users.SingleOrDefaultAsync(x => x.Id == userId);

       
        

        Destination? destinationToEdit = await context.Destinations
            .FirstOrDefaultAsync(x => x.Id.Equals(int.Parse(inputModel.Id)));

        bool isDueDateValid = DateTime
            .TryParseExact(inputModel.PublishedOn, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime publishedOnDate);

        Terrain? terrainValid = await context.Terrains.SingleOrDefaultAsync(x => x.Id.Equals(inputModel.TerrainId));

        if (destinationToEdit != null && isDueDateValid == true && terrainValid != null)
        {
            destinationToEdit.Name = inputModel.Name;
            destinationToEdit.Description = inputModel.Description;
            destinationToEdit.ImageUrl = inputModel.ImageUrl;
            destinationToEdit.PublishedOn = publishedOnDate;
            destinationToEdit.Terrain = terrainValid;

            await context.SaveChangesAsync();
            return true;
        }

        return false;
    }


}