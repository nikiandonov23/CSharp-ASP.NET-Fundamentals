using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CinemaApp.Web.ViewModels.ValidationMessages.Movie;
namespace CinemaApp.Web.Controllers
{
    public class MovieController(IMovieService movieService) : BaseController
    {



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allMovies = await movieService.GetAllMoviesAsync();


            return View(allMovies);
        }



        [HttpGet]
        public async Task<IActionResult> Add()  //Tva samo mu otvarwq nov prozorec zatva e Get
        {

            return View();
        }


        //Същия метод като горния ама овърлоуднат с параметър.
        [HttpPost]
        public async Task<IActionResult> Add(MovieFormViewModel modelToBeAdded)
        {
            if (!ModelState.IsValid)
            {
                return View(modelToBeAdded);
            }

            try
            {


                await movieService.AddAsync(modelToBeAdded);
                return RedirectToAction(nameof(Index));


            }
            catch (Exception e)
            {
                //todo Implement it with the ILogger
                Console.WriteLine(e.Message);


                ModelState.AddModelError(String.Empty, ServiceCreateError);
                return View(modelToBeAdded);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var movie = await movieService.GetMovieDetailsByIdAsync(id);

            if (movie == null)
            {
                return RedirectToAction(nameof(Index)); //s greshto id nqaa se 4upi
            }

            return View(movie);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var movie = await movieService.GetForEditByIdAsync(id);
            if (movie == null)
            {
                return RedirectToAction(nameof(Index)); //s greshto id nqaa se 4upi
            }

            return View(movie);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, MovieFormViewModel movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            await movieService.EditAsync(id, movie);
            return RedirectToAction(nameof(Details), new { id });

        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var movie = await movieService.GetMovieDetailsByIdAsync(id);

            if (movie == null)
            {
                return RedirectToAction(nameof(Index));  //s greshto id nqaa se 4upi
            }

            return View(movie);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await movieService.SoftDeleteAsync(id);

            var remainingMovies = await movieService.GetAllMoviesAsync();
            if (!remainingMovies.Any())
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction(nameof(Index));

        }
    }


}
