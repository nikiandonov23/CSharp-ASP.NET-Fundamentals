using Horizons.Services.Core.Contracts;
using Horizons.Web.ViewModels.Destination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Horizons.Web.Controllers
{
    public class DestinationController(IDestinationService destinationService, ITerrainService terrainService) : BaseController
    {

        //Ако някъде искам да редиректвам към ID
        //   return RedirectToAction(nameof(Details), new { id = inputModel.Id });


        //Ако някъде искам да рефрешна страница щото ми се губят дропдаун менютата

        // if (!ModelState.IsValid)
        //     {
        //         inputModel.Categories = await categoryServices.GetAllCategoriesForCreateAsync();
        //
        //         return View(inputModel);
        //     }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();

            var allDestinations = await destinationService.GetAllDestinationsAsync(userId);

            return View(allDestinations);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var userId = GetUserId();


            var terrainModel = await
                terrainService.GetAllTerrainsForAddAsync();

            var destinationHalfFilledModel = new AddDestinationViewModel()
            {
                Terrains = terrainModel,
                PublishedOn = DateTime.Now.Date
            };


            return View(destinationHalfFilledModel);

        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDestinationViewModel inputModel)
        {
            var userId = GetUserId();
            if (!ModelState.IsValid)
            {
                inputModel.Terrains = await terrainService.GetAllTerrainsForAddAsync();
                return View(inputModel);
            }

            if (userId == null)
            {
                inputModel.Terrains = await terrainService.GetAllTerrainsForAddAsync();
                return View(inputModel);
            }

            var modelToBeAdded = destinationService.AddDestinationAsync(userId, inputModel);

            return RedirectToAction(nameof(Index));

        }



        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var favoriteDestinations = await destinationService.GetFavoritesDestinationsAsync(userId);
            return View(favoriteDestinations);
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int id)
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            await destinationService.AddDestinationToFavoritesAsync(userId, id);
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(int id)
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            await destinationService.RemoveDestinationFromFavoritesAsync(userId, id);
            return RedirectToAction(nameof(Favorites));


        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserId();
            var destinationDetailsToShow = await destinationService.GetDetailsDetailsAsync(id, userId);

            if (destinationDetailsToShow == null) //ako syrvisa mi vurne null
            {
                return RedirectToAction(nameof(Index));
            }

            return View(destinationDetailsToShow);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();

           
            var modelToReturn =await destinationService.GetDeleteDestinationViewModelAsync(id, userId);

            if (userId == null ||modelToReturn==null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(modelToReturn);
        }
       
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteDestinationViewModel inputModel)
        {
            var userId = GetUserId();

            var isDeleteSuccess =await destinationService.DeleteDestinationAsync(inputModel, userId);
            if (isDeleteSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(inputModel);
        }
       
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetUserId();

            if (!ModelState.IsValid || userId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var modelToReturn = await destinationService.GetEditDestinationViewModelAsync(userId, id);
            modelToReturn.Terrains = await terrainService.GetAllTerrainsForEditAsync();


            return View(modelToReturn);


        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditDestinationViewModel inputModel)
        {

            var userId = GetUserId();

            if (!ModelState.IsValid || userId == null)
            {
                inputModel.Terrains = await terrainService.GetAllTerrainsForEditAsync();
                return View(inputModel);
            }

            var isEditingSuccess = await destinationService.EditDestinationAsync(inputModel, userId);
            if (!isEditingSuccess)
            {
                return View(inputModel);
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
