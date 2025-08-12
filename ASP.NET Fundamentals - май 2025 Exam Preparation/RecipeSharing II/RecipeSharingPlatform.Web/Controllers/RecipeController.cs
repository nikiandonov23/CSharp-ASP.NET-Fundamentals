using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels.Recipe;

namespace RecipeSharingPlatform.Web.Controllers
{
    [Authorize]
    public class RecipeController(IRecipeService recipeServices, ICategoryService categoryServices) : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var allRecipes = await recipeServices.GetAllRecipesAsync(userId);
            return View(allRecipes);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var createModel = new CreateRecipeViewModel()
            {
                Categories = await categoryServices.GetAllCategoriesForCreateAsync(),
                CreatedOn = DateTime.Now.Date
            };
            return View(createModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeViewModel inputModel)
        {
            var userId = GetUserId();

            if (!ModelState.IsValid ||userId==null)
            {
                inputModel.Categories = await categoryServices.GetAllCategoriesForCreateAsync();
                return View(inputModel);
            }

            var isItCreated = await recipeServices.CreateRecipeAsync(userId, inputModel);
            if (!isItCreated)
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
            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var allFavorites = await recipeServices.GetFavoritesRecipesAsync(userId);
            return View(allFavorites);
        }

        [HttpPost]
        public async Task<IActionResult> Save(int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            await recipeServices.AddRecipeToFavoritesAsync(userId, id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var isItSuccess = await recipeServices.RemoveRecipeFromFavoritesAsync(userId, id);

            if (!isItSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Favorites));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserId();
            var recipeDetails = await recipeServices.GetRecipeDetailsAsync(id, userId);
            if (recipeDetails == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(recipeDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var modelToDelete = await recipeServices.GetDeleteRecipeViewModelAsync(id, userId);
            if (modelToDelete != null)
            {
                return View(modelToDelete);
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
            var modelToEdit = await recipeServices.GetEditRecipeViewModelAsync(userId, id);
            if (modelToEdit != null)
            {
                modelToEdit.Categories = await categoryServices.GetAllCategoriesForEditAsync();
                return View(modelToEdit);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRecipeViewModel inputModel)
        {
            var userId = GetUserId();

            if (!ModelState.IsValid)
            {
                inputModel.Categories = await categoryServices.GetAllCategoriesForEditAsync();
                return View(inputModel);
            }

            if (userId==null)
            {
                inputModel.Categories = await categoryServices.GetAllCategoriesForEditAsync();
                return View(inputModel);
            }

            var isItSuccess = await recipeServices.EditRecipeAsync(inputModel, userId);

            if (isItSuccess)
            {
                return RedirectToAction(nameof(Details), new { id = inputModel.Id });
            }

            inputModel.Categories = await categoryServices.GetAllCategoriesForEditAsync();
            return View(inputModel);
        }
    }
}
