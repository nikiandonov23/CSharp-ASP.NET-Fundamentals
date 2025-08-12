using Microsoft.EntityFrameworkCore;
using RecipeSharingPlatform.Data;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels.Recipe;

namespace RecipeSharingPlatform.Services.Core;

public  class CategoryService(ApplicationDbContext categoryService): ICategoryService
{
    public async Task<IEnumerable<CreateRecipeCategoryDropDownViewModel>> GetAllCategoriesForCreateAsync()
    {
        var allCategories =await categoryService.Categories
            .Select(x => new CreateRecipeCategoryDropDownViewModel
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
        return allCategories;
    }

    public async Task<IEnumerable<EditRecipeCategoriesDropDownViewModel>> GetAllCategoriesForEditAsync()
    {
        var allCategories = await categoryService.Categories
            .Select(x => new EditRecipeCategoriesDropDownViewModel()
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
        return allCategories;
    }
}