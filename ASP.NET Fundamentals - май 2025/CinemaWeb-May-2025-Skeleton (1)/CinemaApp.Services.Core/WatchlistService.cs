using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Watchlist;
using Microsoft.EntityFrameworkCore;


using static CinemaApp.Web.ViewModels.ValidationMessages.Movie;
namespace CinemaApp.Services.Core;



public class WatchlistService(CinemaAppMay2025DbContext watchListService) : IWatchlistService
{
    public async Task<IEnumerable<WatchlistViewModel>> GetUserWatchListAsync(string userId)
    {
        List<WatchlistViewModel> watchlist = await watchListService
            .ApplicationUserMovies
            .Where(x => x.ApplicationUserId.ToString() == userId)
            .Select(x => new WatchlistViewModel()
            {
                MovieId = x.MovieId.ToString(),
                Title = x.Movie.Title,
                Genre = x.Movie.Genre,
                ReleaseDate = x.Movie.ReleaseDate.ToString(ReleaseDateFormat),
                ImageUrl = x.Movie.ImageUrl


            }).ToListAsync();
        return watchlist;
    }

    public async Task<bool> IsMovieInWatchlistAsync(string userId, Guid movieId)
    {
        return await watchListService.ApplicationUserMovies
            .AnyAsync
             (
             x => x.MovieId == movieId &&
             x.ApplicationUserId == userId &&
             x.IsDeleted == false
             );

    }

    public async Task AddToWatchlistAsync(string userId, string movieId)
    {
        ApplicationUserMovie userMovie = new ApplicationUserMovie()
        {
            ApplicationUserId = userId,
            MovieId = Guid.Parse(movieId)
        };


        await watchListService.ApplicationUserMovies.AddAsync(userMovie);
        await watchListService.SaveChangesAsync();

    }

    public async Task RemoveFromWatchlistAsync(string userId, string movieId)
    {
        var userMovie = await watchListService.ApplicationUserMovies
            .FirstOrDefaultAsync(x => x.ApplicationUserId == userId && x.MovieId == Guid.Parse(movieId));


        if (userMovie != null)
        {
            watchListService.ApplicationUserMovies.Remove(userMovie);
            await watchListService.SaveChangesAsync();
        }
    }
}


