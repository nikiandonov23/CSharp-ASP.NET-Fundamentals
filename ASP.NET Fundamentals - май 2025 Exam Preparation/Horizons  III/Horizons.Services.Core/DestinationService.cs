using System.Collections.Immutable;
using System.Globalization;
using Horizons.Data;
using Horizons.Data.Models;
using Horizons.Services.Core.Contracts;
using Horizons.Web.ViewModels.Destination;
using Microsoft.EntityFrameworkCore;
using static Horizons.GCommon.ValidationConstants;
namespace Horizons.Services.Core;

public class DestinationService(ApplicationDbContext destinationService) : IDestinationService
{
    public async Task<IEnumerable<IndexDestinationViewModel>> GetAllDestinationsAsync(string? currentUserId)
    {
        var allDestinations = await destinationService
            .Destinations
            .Where(x=>x.IsDeleted==false)
            .Select(x => new IndexDestinationViewModel
            {
                Terrain = x.Terrain.Name,
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                FavoritesCount = x.UsersDestinations.Count,
                IsPublisher = x.PublisherId == currentUserId,
                IsFavorite = x.UsersDestinations.Any(x => x.UserId == currentUserId)

            }).ToListAsync();

        return allDestinations;
    }

    public async Task<bool> AddDestinationAsync(string userId, AddDestinationViewModel inputModel)
    {

     

        var destination = new Destination
        {
            Name = inputModel.Name,
            Description = inputModel.Description,
            ImageUrl = inputModel.ImageUrl,
            TerrainId = inputModel.TerrainId,
            PublisherId = userId,
            PublishedOn = inputModel.PublishedOn,
            IsDeleted = false//Tuka da ne iska Terrain.....
        };
        await destinationService.Destinations.AddAsync(destination);
        await destinationService.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<FavoritesDestinationViewModel>> GetFavoritesDestinationsAsync(string userId)
    {
        var favoriteDestinations = await destinationService
            .UsersDestinations
            .Where(x => x.UserId == userId)
            .Select(x => new FavoritesDestinationViewModel()
            {

                Id = x.Destination.Id,
                Name = x.Destination.Name,
                ImageUrl = x.Destination.ImageUrl,
                Terrain = x.Destination.Terrain.Name,

            }).ToListAsync();

        return favoriteDestinations;
    }



    public async Task<DetailsDestinationViewModel?> GetDetailsDetailsAsync(int destinationId, string? userId)
    {
        var isDestinationValid = await destinationService
            .Destinations
            .AnyAsync(x => x.Id == destinationId);



        //ako prez url mi dadat nevalidna destinaciq
        //vrushtam null na cotrolera i modelstate tam nqma da e validen
        {
            if (!isDestinationValid)
                return null;
        }



        var destinationDetails = await destinationService
        .Destinations
        .Include(x => x.Publisher)
        .Where(x => x.Id == destinationId && !x.IsDeleted)
        .Select(x => new DetailsDestinationViewModel()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            ImageUrl = x.ImageUrl,
            Terrain = x.Terrain.Name,
            PublishedOn = x.PublishedOn.Date,
            Publisher = x.Publisher!.Email!.ToLower(),//няма как да е нул щото няма публишер без имейл
            IsPublisher = x.PublisherId == userId,
            IsFavorite = x.UsersDestinations.Any(x => x.UserId == userId)
        }).FirstOrDefaultAsync();

        return destinationDetails;


    }




    public async Task<bool> AddDestinationToFavoritesAsync(string userId, int destinationId)
    {

        var isDestinationIdValid = await destinationService
            .Destinations
            .AnyAsync(x => x.Id == destinationId);

        var isDestinationAlreadyAdded = await destinationService
            .UsersDestinations
            .AnyAsync(x => x.UserId == userId && x.DestinationId == destinationId);

        if (isDestinationAlreadyAdded)
        {
            return false;
        }

        if (!isDestinationIdValid)
        {
            return false;
        }

        await destinationService.UsersDestinations
             .AddAsync(new UserDestination
             {
                 UserId = userId,
                 DestinationId = destinationId
             });

        await destinationService.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveDestinationFromFavoritesAsync(string userId, int destinationId)
    {
        var isDestinationIdValid = await destinationService
            .Destinations
            .AnyAsync(x => x.Id == destinationId);

        var isDestinationAlreadyAdded = await destinationService
            .UsersDestinations
            .AnyAsync(x => x.UserId == userId && x.DestinationId == destinationId);


        if (!isDestinationIdValid)
        {
            return false;

        }

        if (!isDestinationAlreadyAdded)
        {
            return false;
        }

        await destinationService
            .UsersDestinations
            .Where(x => x.UserId == userId && x.DestinationId == destinationId)
            .ExecuteDeleteAsync();

        return true;

    }



    public async Task<EditDestinationViewModel?> GetEditDestinationViewModelAsync(string userId, int destinationId)
    {
        var isDestinationIdValid = await destinationService
            .Destinations
            .AnyAsync(x => x.Id == destinationId);


        if (!isDestinationIdValid)
        {
            return null;
        }



        var modelToReturn = await destinationService.Destinations
            .Where(x => x.Id == destinationId)
            .Select(x => new EditDestinationViewModel()
            {
                Description = x.Description,
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                PublishedOn = x.PublishedOn.Date,
                TerrainId = x.TerrainId,
                PublisherId = x.PublisherId,


            })
            .SingleOrDefaultAsync();
        return modelToReturn;
    }

    public async Task<bool> EditDestinationAsync(EditDestinationViewModel inputModel, string userId)
    {
   



        var destinationToEdit = await destinationService.Destinations
            .Where(x => x.Id == inputModel.Id)
            .SingleOrDefaultAsync();

        if (destinationToEdit == null )
        {
            return false;
        }

        destinationToEdit.Description = inputModel.Description;
        destinationToEdit.ImageUrl = inputModel.ImageUrl;
        destinationToEdit.Name = inputModel.Name;
        destinationToEdit.PublishedOn = inputModel.PublishedOn.Date;
        destinationToEdit.TerrainId = inputModel.TerrainId;

        await destinationService.SaveChangesAsync();
        return true;
    }


    public async Task<DeleteDestinationViewModel?> GetDeleteDestinationViewModelAsync(int destinationId, string? userId)
    {

        var isDestinationIdValid = await destinationService
            .Destinations
            .AnyAsync(x => x.Id == destinationId);
        if (!isDestinationIdValid)
        {
            return null!;
        }


        var modelToShow = await destinationService
            .Destinations
            .Where(x => x.Id == destinationId && x.PublisherId == userId)
            .Select(x => new DeleteDestinationViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Publisher = x.Publisher!.Email!.ToLower(),
                PublisherId = x.PublisherId

            })
            .SingleOrDefaultAsync();

        return modelToShow;
    }

    public async Task<bool> DeleteDestinationAsync(DeleteDestinationViewModel? inputModel, string? userId)
    {
        if (userId == null || inputModel == null)
        {
            return false;
        }

        var destinationToDelete = await destinationService.Destinations
             .Where(x => x.Id == inputModel.Id && x.PublisherId == inputModel.PublisherId)
             .SingleOrDefaultAsync();

        if (destinationToDelete == null)
        {
            return false;
        }

        destinationToDelete.IsDeleted = true;
        await destinationService.SaveChangesAsync();
        return true;
    }
}