using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Watchlist;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class WatchlistController(IWatchlistService watchlistService) : BaseController
    {





        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Index", "Home");
            }


            var userId = GetUserId();
            var watchlist = await watchlistService.GetUserWatchListAsync(userId);

            return View(watchlist);
        }



        [HttpPost]
        public async Task<IActionResult> Add(string movieId)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Index", "Home");
            }


            var userId = GetUserId();
            bool isItInWatchlist = await watchlistService.IsMovieInWatchlistAsync(userId, Guid.Parse(movieId));
            if (!isItInWatchlist)
            {
                await watchlistService.AddToWatchlistAsync(userId, movieId);
               
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task <IActionResult> Remove(string movieId)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Index", "Home");
            }

            var userId= GetUserId();

            bool isMovieInWatchlist = await watchlistService.IsMovieInWatchlistAsync(userId, Guid.Parse(movieId));
            if (isMovieInWatchlist)
            {
                await watchlistService.RemoveFromWatchlistAsync(userId, movieId);
            }

            return RedirectToAction(nameof(Index));
            

        }
    }
}
