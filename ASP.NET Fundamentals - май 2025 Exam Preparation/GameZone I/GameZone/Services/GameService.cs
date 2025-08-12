using GameZone.Data;
using GameZone.Models.Game;
using GameZone.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GameZone.Services;

public class GameService(ApplicationDbContext gameService) : IGameService
{
    public async Task<IEnumerable<AllGamesViewModel>> GetAllGamesAsync(string? currentUserId)
    {
        var allGames = await gameService
             .Games
             .Where(x => x.IsDeleted == false)
             .Select(x => new AllGamesViewModel
             {
                 Id = x.Id,
                 Title = x.Title,
                 ImageUrl = x.ImageUrl,
                 Genre = x.Genre.Name,
                 ReleasedOn = x.ReleasedOn.Date,
                 Publisher = x.Publisher.Email ?? string.Empty,

             })
             .ToListAsync();
        return allGames;
    }

    public async Task<bool> AddGameAsync(string userId, AddGameViewModel inputModel)
    {


        var isUserValid = gameService.Users
            .Any(u => u.Id == userId);

        if (!isUserValid)
        {
            return false;
        }

        var game = new Game
        {
            Title = inputModel.Title,
            Description = inputModel.Description,
            ImageUrl = inputModel.ImageUrl,
            ReleasedOn = inputModel.ReleasedOn.Date,
            GenreId = inputModel.GenreId,
            PublisherId = userId
        };

        await gameService.Games.AddAsync(game);
        await gameService.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<MyZoneViewModel>?> GetMyZoneAsync(string userId)
    {
        bool isUserValid = await gameService.Users
            .AnyAsync(u => u.Id == userId);
        if (!isUserValid)
        {
            return null;
        }


        var myZoneGames = await gameService
            .GamersGames
            .Where(x => x.GamerId == userId &&x.Game.IsDeleted==false)
            .Select(x => new MyZoneViewModel()
            {

                Id = x.Game.Id,
                Title = x.Game.Title,
                ImageUrl = x.Game.ImageUrl,
                Genre = x.Game.Genre.Name,
                ReleasedOn = x.Game.ReleasedOn.Date,
                Publisher = x.Game.Publisher.Email ?? string.Empty,
            }).ToListAsync();

        return myZoneGames;
    }

    public async Task<bool> AddToMyZoneAsync(string userId, int gameId)
    {
        var isGameValid = await gameService
            .Games
            .AnyAsync(x => x.Id == gameId);

        var isGameAlreadyAdded = await gameService
            .GamersGames
            .AnyAsync(x => x.GamerId == userId && x.GameId == gameId);

        if (isGameAlreadyAdded)
        {
            return false;
        }

        if (!isGameValid)
        {
            return false;
        }




        var gameToBeAdded = new GamerGame()
        {
            GamerId = userId,
            GameId = gameId

        };

        await gameService.AddAsync(gameToBeAdded);
        await gameService.SaveChangesAsync();
        return true;


    }

    public async Task<bool> RemoveGameFromMyZoneAsync(string userId, int gameId)
    {

        var isUserValid = await gameService.Users
            .AnyAsync(u => u.Id == userId);


        var isGameValid = await gameService
            .Games
            .AnyAsync(x => x.Id == gameId);

        if (!isUserValid || !isGameValid)
        {
            return false;
        }

        var gameToBeRemoved = await gameService
              .GamersGames
              .Where(x => x.GamerId == userId && x.GameId == gameId)
              .SingleOrDefaultAsync();
        if (gameToBeRemoved == null)
        {
            return false;
        }

        gameService.GamersGames.Remove(gameToBeRemoved);
        await gameService.SaveChangesAsync();
        return true;

    }

    public async Task<DetailsGameViewModel?> GetGameDetailsAsync(int gameId, string? userId)
    {
        var isGameValid = await gameService.Games
            .AnyAsync(x => x.Id == gameId);

        if (!isGameValid)
        {
            return null;
        }

        var gameToReturn = await gameService
            .Games
            .Where(x => x.Id == gameId)
            .Select(x => new DetailsGameViewModel()
            {

                Id = x.Id,
                ReleasedOn = x.ReleasedOn.Date,
                Description = x.Description,
                Genre = x.Genre.Name,
                ImageUrl = x.ImageUrl,
                Publisher = x.Publisher.Email ?? string.Empty,
                Title = x.Title,
                IsDeleted = x.IsDeleted

            }).SingleOrDefaultAsync();
        if (gameToReturn == null)
        {
            return null;
        }

        return gameToReturn;
    }

    public async Task<DeleteGameViewModel?> GetDeleteGameViewModelAsync(int gameId, string? userId)
    {

        var isUserValid = await gameService.Users
            .AnyAsync(u => u.Id == userId);

        var isGameValid = await gameService
            .Games
            .AnyAsync(x => x.Id == gameId && x.PublisherId == userId);


        if (!isGameValid || !isUserValid)
        {
            return null!;
        }


        var gameToDelete = await gameService
            .Games
            .Where(x => x.Id == gameId && x.PublisherId == userId)
            .Select(x => new DeleteGameViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                Publisher = x.Publisher.Email ?? string.Empty,
                ImageUrl = x.ImageUrl
                

            }).SingleOrDefaultAsync();

      
        return gameToDelete;
    }

    public async Task<bool> DeleteGameAsync(DeleteGameViewModel inputModel, string? userId)
    {
        bool isUserValid=gameService.Users
            .Any(u => u.Id == userId);

        if (!isUserValid)
        {
            return false;
        }

        var gameToDelete = await gameService.Games
            .Where(x => x.Id == inputModel.Id && x.PublisherId == userId)
            .SingleOrDefaultAsync();

        if (gameToDelete == null)
        {
            return false;
        }

        gameToDelete.IsDeleted = true;

        await gameService.SaveChangesAsync();
        return true;
    }

    public async Task<EditGameViewModel?> GetEditGenreViewModelAsync(string userId, int gameId)
    {
        var isGameValid= await gameService
            .Games
            .AnyAsync(x => x.Id == gameId && x.PublisherId == userId);

        var isUserValid = await gameService
            .Users
            .AnyAsync(x => x.Id == userId);

        if (!isGameValid || !isUserValid)
        {
            return null!;
        }




        var gameToReturn =await gameService
            .Games
            .Where(x => x.Id == gameId && x.PublisherId == userId)
            .Select(x => new EditGameViewModel()
            {

                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                ReleasedOn = x.ReleasedOn.Date,
                GenreId = x.GenreId,
                IsDeleted = x.IsDeleted,
            }).SingleOrDefaultAsync();
        return gameToReturn;

    }

    public async Task<bool> EditGameAsync(EditGameViewModel inputModel, string userId)
    {

        var isUserValid = await gameService
            .Users
            .AnyAsync(x => x.Id == userId);

        if (!isUserValid)
        {
            return false;
        }

        var gameToChange =await gameService
            .Games
            .Where(x => x.Id==inputModel.Id &&x.PublisherId==userId)
            .FirstOrDefaultAsync();

        if (gameToChange == null)
        {
            return false;
        }
        
        gameToChange.Title = inputModel.Title;
        gameToChange.Description = inputModel.Description;
        gameToChange.ImageUrl = inputModel.ImageUrl;
        gameToChange.ReleasedOn = inputModel.ReleasedOn.Date;
        gameToChange.GenreId = inputModel.GenreId;

        await gameService.SaveChangesAsync();

        return true;
    }
}
