using System.Globalization;
using System.Security.Claims;
using Horizons.Data;
using Horizons.Data.Models;
using Horizons.Services.Core.Contracts;
using Horizons.Web.ViewModels.Destination;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Services.Core;

public class DestinationService(ApplicationDbContext destinationService) : IDestinationService
{
    public async Task<IEnumerable<IndexDestinationViewModel>> GetAllDestinationsAsync(string? currentUserId)
    {


        IEnumerable<IndexDestinationViewModel> allModels = await destinationService
            .Destinations
            .Where(x=>x.IsDeleted==false)
            .Select(x => new IndexDestinationViewModel()
            {
                FavoritesCount = x.UsersDestinations.Count,
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                IsFavorite = currentUserId != null && x.UsersDestinations.Any(x => x.UserId == currentUserId),
                IsPublisher = currentUserId != null && x.PublisherId == currentUserId,
                Name = x.Name,
                Terrain = x.Terrain.Name,


            }).ToListAsync();
        return allModels;


    }

    public async Task<bool> AddDestinationAsync(string currentUserId, AddDestinationViewModel inputModel)
    {

        bool isPublishedOnValid = DateTime
            .TryParseExact(inputModel.PublishedOn.ToString("yyyy-MM-dd"),
                "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var publishedOnDate);

        bool isPublisherIdValid = destinationService.Users.Any(x => x.Id == currentUserId);

        if (!isPublishedOnValid || !isPublisherIdValid)
        {
            return false;
        }

        var modelToBeAdded = new Destination()
        {

            //Id-to nqma nujda da go zadavam, shte se generira avtomatichno
            Name = inputModel.Name,
            Description = inputModel.Description,
            ImageUrl = inputModel.ImageUrl,
            PublishedOn = publishedOnDate,


            PublisherId = currentUserId,
            TerrainId = inputModel.TerrainId,
            


        };
        await destinationService.Destinations.AddAsync(modelToBeAdded);
        await destinationService.SaveChangesAsync();

        return true;


    }


    public async Task<DetailsDestinationViewModel?> GetDestinationDetailsAsync(int destinationId, string? currentUserId)
    {

        bool isDestinationIdValid = await destinationService
            .Destinations
            .AnyAsync(x => x.Id == destinationId);
        if (!isDestinationIdValid)
        {
            return null;
        }

        var destinationToShow = await destinationService
            .Destinations
            .Where(x => x.Id == destinationId)
            .Select(x => new DetailsDestinationViewModel()
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                PublishedOn = x.PublishedOn,
                Terrain = x.Terrain.Name,
                // Фикс за CS8601: Проверка за null на Publisher и Email
                Publisher = x.Publisher!.Email ?? string.Empty,
                IsPublisher = currentUserId != null && x.PublisherId == currentUserId,
                IsFavorite = currentUserId != null && x.UsersDestinations.Any(x => x.UserId == currentUserId),

            }).SingleOrDefaultAsync();

        return destinationToShow;
    }

    public async Task<IEnumerable<FavoritesDestinationViewModel>> GetFavoritesDestinationsAsync(string currentUserId)
    {
        var favoriteDestinations = await destinationService
            .UsersDestinations

            //Vis4ki destinationUsers na koito UserId == currentUserId
            .Where(x => x.UserId == currentUserId)
            .Select(x => new FavoritesDestinationViewModel()
            {
                //burkame  verijno prez tablicite ot ednata prez neq ta v drugata
                Id = x.Destination.Id,
                ImageUrl = x.Destination.ImageUrl,
                Name = x.Destination.Name,
                Terrain = x.Destination.Terrain.Name



            }).ToListAsync();
        return favoriteDestinations;
    }

    public async Task<bool> AddDestinationToFavoritesAsync(string userId, int destinationId)
    {
        bool isUserIdValid = await destinationService
            .Users
            .AnyAsync(x => x.Id == userId);
        bool isDestinationIdValid = destinationService.Destinations

            .Any(x => x.Id == destinationId);

        if (!isDestinationIdValid || !isUserIdValid)
        {
            return false;
        }

        await destinationService
            .UsersDestinations
            .AddAsync(new UserDestination()
            {
                UserId = userId,
                DestinationId = destinationId
            });
        await destinationService.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveDestinationFromFavoritesAsync(string userId, int destinationId)
    {
        bool isUserIdValid = await destinationService
            .Users
            .AnyAsync(x => x.Id == userId);
        bool isDestinationIdValid = destinationService.Destinations

            .Any(x => x.Id == destinationId);

        if (!isDestinationIdValid || !isUserIdValid)
        {
            return false;
        }




        await destinationService.UsersDestinations
            .Where(x => x.DestinationId == destinationId && x.UserId == userId)
            .ExecuteDeleteAsync();
        await destinationService.SaveChangesAsync();
        return true;
    }

    public async Task<EditDestinationViewModel?> GetEditDestinationViewModel(string userId, int destinationId)
    {
        var terrains = await destinationService.Terrains
            .Select(t => new EditDestinationTerrainDropDownModel
            {
                Id = t.Id,
                Name = t.Name
            }).ToListAsync();


        var destinationToEdit = await destinationService
            .Destinations
            .Where(x => x.Id == destinationId)
            .Select(x => new EditDestinationViewModel()
            {
                Description = x.Description,
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                PublishedOn = x.PublishedOn,
                PublisherId = x.PublisherId,
                TerrainId = x.TerrainId,
                Terrains = terrains,

            }).SingleOrDefaultAsync();
        return destinationToEdit;
    }

    public async Task<bool> EditDestinationAsync(EditDestinationViewModel inputModel, string userId)
    {
        var destinationToEdit = await destinationService.Destinations
            .SingleOrDefaultAsync(x => x.Id == inputModel.Id);

        if (destinationToEdit == null) return false;

        destinationToEdit.Name = inputModel.Name;
        destinationToEdit.Description = inputModel.Description;
        destinationToEdit.ImageUrl = inputModel.ImageUrl;
        destinationToEdit.PublishedOn = inputModel.PublishedOn;
        destinationToEdit.TerrainId = inputModel.TerrainId;

        await destinationService.SaveChangesAsync();

        return true;


    }

    public Task<DeleteDestinationViewModel?> GetDeleteDestinationViewModel(string userId, int destinationId)
    {
        var destinationToShow = destinationService
            .Destinations
            .Where(x => x.Id.Equals(destinationId))
            .Select(x => new DeleteDestinationViewModel()
            {

                Id = x.Id,
                Name = x.Name,
                Publisher = x.Publisher!.Email ?? string.Empty,
                PublisherId = x.PublisherId,
            }).SingleOrDefaultAsync();
        return destinationToShow;
    }

    public async Task<bool> DeleteDestinationAsync(string userId, DeleteDestinationViewModel inputModel)
    {
        var destinationToDelete=await destinationService
            .Destinations
            .Where(x=>x.Id==inputModel.Id)
            .SingleOrDefaultAsync();

        if (destinationToDelete == null || destinationToDelete.PublisherId != userId)
        {
            return false;
        }

        destinationToDelete.IsDeleted = true;

        await destinationService.SaveChangesAsync();

        return true;
    }
}
