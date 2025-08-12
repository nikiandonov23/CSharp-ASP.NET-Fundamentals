using GameZone.Models.Game;
using GameZone.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class GameController(IGameService gameService, IGenreService genreService) : BaseController
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
        public async Task<IActionResult> All()
        {
            var userId = GetUserId();


            var allGames = await gameService.GetAllGamesAsync(userId);
            return View(allGames);
        }



        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var userId = GetUserId();


            var createModel = new AddGameViewModel()
            {
                Genres = await genreService.GetAllGenresForAddAsync(),
                ReleasedOn = DateTime.Now.Date

            };

            return View(createModel);

        }

        [HttpPost]
        public async Task<IActionResult> Add(AddGameViewModel inputModel)
        {
            var userId = GetUserId();

            //Tuk валидации за !ModelState.IsValid и userId==null

            if (!ModelState.IsValid || userId == null)
            {
                inputModel.Genres = await genreService.GetAllGenresForAddAsync();
                return View("Add", inputModel);
            }

            await gameService.AddGameAsync(userId, inputModel);
            return RedirectToAction(nameof(All));
        }







        [HttpGet]
        public async Task<IActionResult> MyZone()
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return RedirectToAction(nameof(All));
            }

            var myZone = await gameService.GetMyZoneAsync(userId);

            if (myZone == null)
            {
                return RedirectToAction(nameof(All));

            }
            return View(myZone);

        }

        [HttpGet]
        public async Task<IActionResult> AddToMyZone(int id)
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return RedirectToAction(nameof(All));
            }

            var isItSuccess = await gameService.AddToMyZoneAsync(userId, id);
            if (!isItSuccess)
            {
                return RedirectToAction(nameof(All));
            }

            return RedirectToAction(nameof(MyZone));

        }

        [HttpGet]
        public async Task<IActionResult> StrikeOut(int id)
        {
            var userId = GetUserId();
            if (userId == null || !ModelState.IsValid)
            {
                return RedirectToAction(nameof(All));
            }

            var isItSuccess = await gameService.RemoveGameFromMyZoneAsync(userId, id);

            if (!isItSuccess)
            {
                return RedirectToAction(nameof(All));
            }
            return RedirectToAction(nameof(MyZone));

        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return RedirectToAction(nameof(All));
            }

            var gameToShow = await gameService
                .GetGameDetailsAsync(id, userId);


            if (gameToShow == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(gameToShow);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();


            if (userId == null)
            {
                return RedirectToAction(nameof(All));
            }

            var gameToDelete = await gameService.GetDeleteGameViewModelAsync(id, userId);

            if (gameToDelete == null)
            {
                return RedirectToAction(nameof(All));
            }
            return View(gameToDelete);



        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(DeleteGameViewModel inputModel)
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return RedirectToAction(nameof(All));
            }

            var isItSuccess = await gameService.DeleteGameAsync(inputModel, userId);

            if (!isItSuccess)
            {
                return RedirectToAction(nameof(All));

            }

            return RedirectToAction(nameof(MyZone));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetUserId();
            if (userId==null)
            {
                return RedirectToAction(nameof(All));
            }

            var modelToEdit =await gameService.GetEditGenreViewModelAsync(userId, id);
            if (modelToEdit == null)
            {
                return RedirectToAction(nameof(All));
            }
            modelToEdit.Genres = await genreService.GetAllGenresForEditAsync();
          
            return View(modelToEdit);


        }

             [HttpPost]
             public async Task<IActionResult> Edit(EditGameViewModel inputModel)
             {
        
                 var userId = GetUserId();

                 if (userId==null || !ModelState.IsValid )
                 {
                     return RedirectToAction(nameof(Edit), new { id = inputModel.Id });
                 }

                 var isItSuccess = await gameService.EditGameAsync(inputModel, userId);

                 if (!isItSuccess)
                 {
                     return RedirectToAction(nameof(Edit), new { id = inputModel.Id });
                 }

                 return RedirectToAction(nameof(All));
             }

    }
}
