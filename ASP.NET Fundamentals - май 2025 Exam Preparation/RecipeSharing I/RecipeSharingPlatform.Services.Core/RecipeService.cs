using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RecipeSharingPlatform.Data;
using RecipeSharingPlatform.Data.Models;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels.Recipe;
using static RecipeSharingPlatform.GCommon.ValidationConstants;

namespace RecipeSharingPlatform.Services.Core
{
    public class RecipeService(ApplicationDbContext recipeServices) : IRecipeService
    {
        public async Task<IEnumerable<IndexRecipeViewModel>> GetAllRecipesAsync(string? currentUserId)
        {
            var allRecipes = await recipeServices.Recipes
                .Where(x => x.IsDeleted == false)
                .Select(x => new IndexRecipeViewModel()
                {
                    Id = x.Id,
                    Category = x.Category.Name,
                    ImageUrl = x.ImageUrl,
                    IsAuthor = x.AuthorId.Equals(currentUserId),
                    IsSaved = x.UsersRecipes.Any(x => x.UserId == currentUserId),
                    Title = x.Title,
                    SavedCount = x.UsersRecipes.Count(),

                }).ToListAsync();

            return allRecipes;
        }

        public async Task<bool> CreateRecipeAsync(string? userId, CreateRecipeViewModel inputModel)
        {
            bool isCreatedOnValid = DateTime
                .TryParseExact(inputModel.CreatedOn, RecipeCreatedOnFormat, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime dueDate);

            if (!isCreatedOnValid || userId == null)
            {
                return false;
            }

            var recipeToBeAdded = new Recipe()
            {
                Title = inputModel.Title,
                Instructions = inputModel.Instructions,
                ImageUrl = inputModel.ImageUrl,
                AuthorId = userId,
                CategoryId = inputModel.CategoryId,
                CreatedOn = dueDate,
                IsDeleted = false
            };

            await recipeServices.AddAsync(recipeToBeAdded);
            await recipeServices.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FavoritesRecipeViewModel>> GetFavoritesRecipesAsync(string userId)
        {



            var allFavoritesRecipes = await recipeServices
                .UsersRecipes
                .Where(x => x.UserId == userId)
                .Select(x => new FavoritesRecipeViewModel()
                {
                    Id = x.RecipeId,
                    Category = x.Recipe.Category.Name,
                    ImageUrl = x.Recipe.ImageUrl,
                    Title = x.Recipe.Title,


                }).ToListAsync();
            return allFavoritesRecipes;
        }

        public async Task<bool> AddRecipeToFavoritesAsync(string userId, int recipeId)
        {
            bool isUserValid = await recipeServices
                .Users
                .Where(x => x.Id == userId)
                .AnyAsync();

            bool isRecipeValid = await recipeServices
                .Recipes
                .Where(x => x.Id == recipeId)
                .AnyAsync();

            bool isRecipeAlreadyAdded = await recipeServices
                .UsersRecipes
                .AnyAsync(x => x.UserId == userId && x.RecipeId == recipeId);



            if (!isUserValid || !isRecipeValid || isRecipeAlreadyAdded)
            {
                return false;
            }

            var userRecipe = new UserRecipe()
            {
                RecipeId = recipeId,
                UserId = userId
            };
            await recipeServices.AddAsync(userRecipe);
            await recipeServices.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRecipeFromFavoritesAsync(string userId, int recipeId)
        {
            bool isUserValid = await recipeServices
                .Users
                .Where(x => x.Id == userId)
                .AnyAsync();

            bool isRecipeValid = await recipeServices
                .Recipes
                .Where(x => x.Id == recipeId)
                .AnyAsync();


            if (!isUserValid || !isRecipeValid)
            {
                return false;
            }

            await recipeServices.UsersRecipes
                .Where(x => x.UserId == userId && x.RecipeId == recipeId)
                .ExecuteDeleteAsync();
            await recipeServices.SaveChangesAsync();
            return true;
        }

        public async Task<DetailsRecipeViewModel?> GetRecipeDetailsAsync(int recipeId, string? userId)
        {
            bool isRecipeValid = await recipeServices
                .Recipes
                .Where(x => x.Id == recipeId)
                .AnyAsync();


            if (!isRecipeValid)
            {
                return null!;
            }

            var recipeDetails = await recipeServices
                .Recipes
                .Where(x => x.Id == recipeId)
                .Select(x => new DetailsRecipeViewModel()
                {
                    Author = x.Author!.Email ?? string.Empty,
                    Category = x.Category.Name,
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy"),
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Instructions = x.Instructions,
                    IsAuthor = x.AuthorId.Equals(userId),
                    IsSaved = x.UsersRecipes.Any(x => x.UserId == userId),
                    Title = x.Title

                }).SingleOrDefaultAsync();
            return recipeDetails;
        }

        public async Task<DeleteRecipeViewModel?> GetDeleteRecipeViewModelAsync(int recipeId, string? userId)
        {
            //dobavi proverka za isUserIdValid


            var deleteModelToShow = await recipeServices
                .Recipes
                .Where(x => x.AuthorId == userId && x.Id == recipeId)
                .Select(x => new DeleteRecipeViewModel()
                {
                    Author = x.Author!.Email ?? string.Empty,
                    AuthorId = x.AuthorId,
                    Id = x.Id,
                    Title = x.Title

                }).SingleOrDefaultAsync();

            return deleteModelToShow;
        }

        public async Task<bool> DeleteRecipeAsync(DeleteRecipeViewModel inputModel, string? userId)
        {
            var recipeToDelete = await recipeServices
                .Recipes
                .Where(x => x.Id == inputModel.Id)
                .SingleOrDefaultAsync();

            if (recipeToDelete == null || recipeToDelete.AuthorId != userId)
            {
                return false;
            }

            recipeToDelete.IsDeleted = true;
            await recipeServices.SaveChangesAsync();
            return true;
        }

        public async Task<EditRecipeViewModel?> GetEditRecipeViewModelAsync(string userId, int recipeId)
        {
            var categories = await recipeServices
                .Categories
                .Select(x => new EditRecipeCategoriesDropDownViewModel()
                {
                    Id = x.Id,
                    Name = x.Name

                }).ToListAsync();

            var recipeModelToReturn = await recipeServices
                .Recipes
                .Where(x => x.Id == recipeId && x.AuthorId == userId)
                .Select(x => new EditRecipeViewModel()
                {
                    Id = x.Id,
                    Categories = categories,
                    CategoryId = x.CategoryId,
                    CreatedOn = x.CreatedOn,
                    ImageUrl = x.ImageUrl,
                    Instructions = x.Instructions,
                    Title = x.Title


                }).SingleOrDefaultAsync();

            return recipeModelToReturn;

        }

        public async Task<bool> EditRecipeAsync(EditRecipeViewModel inputModel, string userId)
        {

          

            var recipeToEdit = await recipeServices
                .Recipes
                .Where(x => x.Id == inputModel.Id && x.AuthorId == userId)
                .SingleOrDefaultAsync();
           
            if (recipeToEdit == null)
            {
                return false;
            }
            


            recipeToEdit.CategoryId = inputModel.CategoryId;
            recipeToEdit.CreatedOn = inputModel.CreatedOn;
            recipeToEdit.ImageUrl = inputModel.ImageUrl;
            recipeToEdit.Instructions = inputModel.Instructions;
            recipeToEdit.Title = inputModel.Title;

            await recipeServices.SaveChangesAsync();
            return true;

        }
    }
}
