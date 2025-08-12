using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RecipeSharingPlatform.Data;
using RecipeSharingPlatform.Data.Models;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels.Recipe;

namespace RecipeSharingPlatform.Services.Core
{
    public class RecipeService(ApplicationDbContext recipeService) : IRecipeService
    {
        public async Task<IEnumerable<IndexRecipeViewModel>> GetAllRecipesAsync(string? currentUserId)
        {


            var allRecipes = await recipeService.Recipes
                .Where(x => x.IsDeleted == false)
                .Select(x => new IndexRecipeViewModel
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    SavedCount = x.UsersRecipes.Count,
                    Title = x.Title,
                    Category = x.Category.Name,
                    IsAuthor = x.AuthorId == currentUserId,
                    IsSaved = x.UsersRecipes.Any(x => x.UserId == currentUserId),




                }).ToListAsync();

            return allRecipes;
        }

        public async Task<bool> CreateRecipeAsync(string userId, CreateRecipeViewModel inputModel)
        {


            var isUserValid = await recipeService
                .Users.AnyAsync(x => x.Id == userId);

            var isCategoryValid = await recipeService
                .Categories.AnyAsync(x => x.Id == inputModel.CategoryId);

            if (!isUserValid || !isCategoryValid)
            {
                return false;
            }



            var newRecipe = new Recipe()
            {
                AuthorId = userId,
                Title = inputModel.Title,
                CategoryId = inputModel.CategoryId,
                Instructions = inputModel.Instructions,
                ImageUrl = inputModel.ImageUrl,
                CreatedOn = inputModel.CreatedOn.Date,
                IsDeleted = false,

            };

            await recipeService.Recipes.AddAsync(newRecipe);
            await recipeService.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<FavoritesRecipeViewModel>?> GetFavoritesRecipesAsync(string userId)
        {

            var isUserValid = await recipeService.Users
                .AnyAsync(x => x.Id == userId);
            if (!isUserValid)
            {
                return null!;
            }

            var allFavorites = await recipeService.UsersRecipes
                .Where(x => x.UserId == userId)
                .Select(x => new FavoritesRecipeViewModel()
                {
                    Category = x.Recipe.Category.Name,
                    Title = x.Recipe.Title,
                    Id = x.RecipeId,
                    ImageUrl = x.Recipe.ImageUrl

                }).ToListAsync();


            return allFavorites;
        }

        public async Task<bool> AddRecipeToFavoritesAsync(string userId, int recipeId)
        {
            var isUserValid = await recipeService.Users
                .AnyAsync(x => x.Id == userId);

            var isRecipeAlreadyAdded = await recipeService
                .Recipes
                .AnyAsync(x => x.Id == recipeId && x.AuthorId == userId);

            var isRecipeValid = await recipeService //true ako e valid
                .Recipes
                .AnyAsync(x => x.Id == recipeId);

            if (!isUserValid || isRecipeAlreadyAdded || !isRecipeValid)
            {
                return false;
            }



            var recipeToBeAdded = new UserRecipe()
            {
                RecipeId = recipeId,
                UserId = userId,
            };
            await recipeService.UsersRecipes.AddAsync(recipeToBeAdded);
            await recipeService.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRecipeFromFavoritesAsync(string userId, int recipeId)
        {
            var isUserValid = await recipeService.Users
                .AnyAsync(x => x.Id == userId);

            var isRecipeValid = await recipeService //true ako e valid
                .Recipes
                .AnyAsync(x => x.Id == recipeId);


            var isRecipeAlreadyAdded = await recipeService
                .UsersRecipes
                .AnyAsync(x => x.RecipeId == recipeId && x.UserId == userId);

            if (!isRecipeAlreadyAdded || !isRecipeValid || !isUserValid)
            {
                return false;
            }


            await recipeService.UsersRecipes
                .Where(x => x.RecipeId == recipeId && x.UserId == userId)
                .ExecuteDeleteAsync();
            await recipeService.SaveChangesAsync();
            return true;
        }

        public async Task<DetailsRecipeViewModel?> GetRecipeDetailsAsync(int recipeId, string? userId)
        {


            var isUserValid = await recipeService.Users
                .AnyAsync(x => x.Id == userId);

            var isRecipeValid = await recipeService //true ako e valid
                .Recipes
                .AnyAsync(x => x.Id == recipeId);

            if (!isUserValid || !isRecipeValid)
            {
                return null;
            }


            var modelToShow = await recipeService
                .Recipes
                .Where(x => x.Id == recipeId && x.IsDeleted == false)
                .Select(x => new DetailsRecipeViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Instructions = x.Instructions,
                    ImageUrl = x.ImageUrl,
                    Category = x.Category.Name,
                    CreatedOn = x.CreatedOn.Date,
                    Author = x.Author.Email ?? string.Empty,
                    IsSaved = x.UsersRecipes.Any(x => x.UserId == userId),
                    IsAuthor = x.AuthorId.Equals(userId),


                }).SingleOrDefaultAsync();

            if (modelToShow == null)
            {
                return null!;
            }

            return modelToShow;
        }

        public async Task<DeleteRecipeViewModel?> GetDeleteRecipeViewModelAsync(int recipeId, string? userId)
        {
            
            
            var isUserValid = await recipeService.Users
                .AnyAsync(x => x.Id == userId);
            var isRecipeValid = await recipeService
                .Recipes
                .AnyAsync(x => x.Id == recipeId && x.AuthorId == userId && x.IsDeleted == false);

            if (!isUserValid || !isRecipeValid)
            {
                return null;
            }


            var modelToDelete = await recipeService
                .Recipes
                .Where(x => x.Id == recipeId && x.AuthorId == userId && x.IsDeleted == false)
                .Select(x => new DeleteRecipeViewModel()
                {
                   
                    Id = x.Id,
                    Title = x.Title,
                    AuthorId = x.AuthorId


                }).SingleOrDefaultAsync();

            if (modelToDelete==null)
            {
                return null;
            }
            return modelToDelete;
        }

        public async Task<bool> DeleteRecipeAsync(DeleteRecipeViewModel inputModel, string? userId)
        {

            var isUserValid = await recipeService
                .Users
                .AnyAsync(x => x.Id == userId);

            var isRecipeValid = await recipeService
                .Recipes
                .AnyAsync(x => x.Id == inputModel.Id && x.AuthorId == userId && x.IsDeleted == false);

            if (!isUserValid || !isRecipeValid)
            {
                return false;
            }



            var modelToDelete= await recipeService
                .Recipes
                .Where(x => x.Id == inputModel.Id && x.AuthorId == userId && x.IsDeleted == false)
                .SingleOrDefaultAsync();
            if (modelToDelete == null)
            {
                return false;
            }
            modelToDelete.IsDeleted = true;
            await recipeService.SaveChangesAsync();
            return true;
        }

        public async Task<EditRecipeViewModel?> GetEditRecipeViewModelAsync(string userId, int recipeId)
        {




            var isUserValid = await recipeService.Users
                .AnyAsync(x => x.Id == userId);

            var isRecipeValid = await recipeService //true ako e valid
                .Recipes
                .AnyAsync(x => x.Id == recipeId);


            if (!isRecipeValid || !isUserValid)
            {
                return null;
            }


            var modelToEdit = await recipeService
                .Recipes
                .Where(x => x.Id == recipeId && x.AuthorId == userId && x.IsDeleted == false)
                .Select(x => new EditRecipeViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Instructions = x.Instructions,
                    ImageUrl = x.ImageUrl,
                    CategoryId = x.CategoryId,
                    CreatedOn = x.CreatedOn.Date,

                }).SingleOrDefaultAsync();
            if (modelToEdit == null)
            {
                return null;
            }

            return modelToEdit;
        }

        public async Task<bool> EditRecipeAsync(EditRecipeViewModel inputModel, string userId)
        {

            var isUserValid = await recipeService.Users
                .AnyAsync(x => x.Id == userId);

            var isRecipeValid = await recipeService
                .Recipes
                .AnyAsync(x => x.Id == inputModel.Id && x.AuthorId == userId && x.IsDeleted == false);

            if (!isUserValid || !isRecipeValid)
            {
                return false;
            }





            var modelToEdit = await recipeService
                .Recipes
                .Where(x => x.Id == inputModel.Id && x.AuthorId == userId && x.IsDeleted == false)
                .SingleOrDefaultAsync();

            if (modelToEdit == null)
            {
                return false;
            }
            modelToEdit.Title = inputModel.Title;
            modelToEdit.Instructions = inputModel.Instructions;
            modelToEdit.ImageUrl = inputModel.ImageUrl;
            modelToEdit.CategoryId = inputModel.CategoryId;
            modelToEdit.CreatedOn = inputModel.CreatedOn.Date;
            await recipeService.SaveChangesAsync();
            return true;
        }
    }
}
