using Microsoft.EntityFrameworkCore;
using RecipeSharingPlatform.Data;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels.Recipe;

namespace RecipeSharingPlatform.Services.Core;

public class CategoryService(ApplicationDbContext context):ICategoryService
{
    public async Task<IEnumerable<CreateRecipeCategoryDropDownModel>> GetAllCategoriesForCreateAsync()
    {
        var allCategories = await context
            .Categories
            .Select(x => new CreateRecipeCategoryDropDownModel()
            {
                Id = x.Id,
                Name = x.Name,


            }).ToListAsync();

        return allCategories;
    }

    public async Task<IEnumerable<EditRecipeCategoriesDropDownViewModel>> GetAllCategoriesForEditAsync()
    {
        var categoriesForEdit = await context
            .Categories
            .Select(x => new EditRecipeCategoriesDropDownViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
        return categoriesForEdit;
    }
}