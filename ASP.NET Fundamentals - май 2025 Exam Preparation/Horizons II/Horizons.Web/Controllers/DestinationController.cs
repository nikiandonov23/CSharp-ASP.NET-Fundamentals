using System.Globalization;
using Horizons.Services.Core.Contracts;
using Horizons.Web.ViewModels.Destination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Horizons.GCommon.ValidationConstants;
namespace Horizons.Web.Controllers
{

    [AllowAnonymous]
    public class DestinationController(IDestinationService destinationService, ITerrainService terrainService) : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var allDestinations = await destinationService.GetAllDestinationsAsync(GetUserId());


            return View(allDestinations);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {

            //vzemam si polupopulnen model za da go pokazvam na formata
            var halfFilledModelToShow = new AddDestinationViewModel()
            {
                Terrains = await terrainService.GetAllTerrainsAsync(),
                PublishedOn = DateTime.UtcNow.Date

            };
            return View(halfFilledModelToShow);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDestinationViewModel inputModel)
        {


            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId) || !ModelState.IsValid)
            {
                return RedirectToAction("Index", "Destination");
            }

            await destinationService.AddDestinationAsync(userId, inputModel);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserId();


            var destinationToReturn = await destinationService.GetDestinationDetailsAsync(id, userId);


            if (destinationToReturn == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(destinationToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = GetUserId();



            if (userId != null)
            {
                var favoritesToReturn =
                    await destinationService.GetFavoritesDestinationsAsync(userId);
                return View(favoritesToReturn);
            }

            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int id)
        {


            var userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var isAdded = await destinationService.AddDestinationToFavoritesAsync(userId, id);
            if (!isAdded)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Favorites));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(int id, string currentUserId)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction(nameof(Index));

            }

            var isRemoved = await destinationService.RemoveDestinationFromFavoritesAsync(userId, id);
            if (!isRemoved)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Favorites));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetUserId();

            var destinationToShow = await destinationService
                .GetEditDestinationViewModel(userId, id);

            if (destinationToShow == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(destinationToShow);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditDestinationViewModel inputModel)
        {
            var currentUserId = GetUserId();
            if (currentUserId == null || !ModelState.IsValid )
            {
                return RedirectToAction(nameof(Edit));
            }

            var isDestinationEdited = await destinationService.EditDestinationAsync(inputModel, currentUserId);

            if (!isDestinationEdited)
            {
                return RedirectToAction(nameof(Edit));
            }

            return RedirectToAction(nameof(Details), new { id = inputModel.Id });

        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();


            var destinationToShow = await destinationService.GetDeleteDestinationViewModel(userId, id);
            if (destinationToShow == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(destinationToShow);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteDestinationViewModel inputModel)
        {
            var userId = GetUserId();

            var isDeleted = await destinationService.DeleteDestinationAsync(userId, inputModel);

            if (userId == null ||!isDeleted)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));

        }

    }

}
