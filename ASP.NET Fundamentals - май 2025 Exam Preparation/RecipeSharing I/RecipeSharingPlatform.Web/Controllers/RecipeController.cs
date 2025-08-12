using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels.Recipe;
using static RecipeSharingPlatform.GCommon.ValidationConstants;

namespace RecipeSharingPlatform.Web.Controllers
{
    public class RecipeController(IRecipeService recipeServices, ICategoryService categoryServices) : BaseController
    {
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();


            var recipesToShow = await recipeServices
                .GetAllRecipesAsync(userId);

            return View(recipesToShow);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = GetUserId();


            var createModel = new CreateRecipeViewModel()
            {
                Categories = await categoryServices.GetAllCategoriesForCreateAsync(),
                CreatedOn = DateTime.Now.ToString("yyyy-MM-dd")

            };

            return View(createModel);

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeViewModel inputModel)
        {
            var userId = GetUserId();



            if (!ModelState.IsValid)
            {
                inputModel.Categories = await categoryServices.GetAllCategoriesForCreateAsync();

                return View(inputModel);
            }
            var isRecipeCreated = await recipeServices.CreateRecipeAsync(userId, inputModel);

            if (!isRecipeCreated)
            {
                inputModel.Categories = await categoryServices.GetAllCategoriesForCreateAsync();
                return View(inputModel);

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = GetUserId();
            if (userId != null)
            {
                var favorites = await recipeServices.GetFavoritesRecipesAsync(userId);
                return View(favorites);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Save(int id)
        {
            var userId = GetUserId();

            if (userId != null)
            {
                var isAddSuccess = await recipeServices.AddRecipeToFavoritesAsync(userId, id);
                if (isAddSuccess)
                {
                    return RedirectToAction(nameof(Favorites));
                }

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var userId = GetUserId();

            if (userId != null)
            {
                var isRemovedSuccess = await recipeServices.RemoveRecipeFromFavoritesAsync(userId, id);
                if (isRemovedSuccess)
                {
                    return RedirectToAction(nameof(Favorites));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserId();

            var destinationToReturn = await recipeServices.GetRecipeDetailsAsync(id, userId);
            if (destinationToReturn != null)
            {
                return View(destinationToReturn);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();


            if (userId != null)
            {
                var modelToReturn = await recipeServices
                    .GetDeleteRecipeViewModelAsync(id, userId);
                return View(modelToReturn);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(DeleteRecipeViewModel inputModel)
        {
            var userId = GetUserId();

            var isItSuccess = await recipeServices.DeleteRecipeAsync(inputModel, userId);
            if (isItSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(ConfirmDelete));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetUserId();
            if (userId==null)
            {
                return RedirectToAction(nameof(Index));
            }

            var modelToShow =await recipeServices.GetEditRecipeViewModelAsync(userId, id);
            return View(modelToShow);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRecipeViewModel inputModel)
        {

            var userId = GetUserId();
            if (userId == null)
            {
                return View(inputModel);

            }

            var isEditSuccessful =await recipeServices.EditRecipeAsync(inputModel, userId);
            if (!isEditSuccessful||!ModelState.IsValid)
            {
                inputModel.Categories = await categoryServices.GetAllCategoriesForEditAsync();

                return View(inputModel);

            }

            return RedirectToAction(nameof(Index));
        }
    }
}
