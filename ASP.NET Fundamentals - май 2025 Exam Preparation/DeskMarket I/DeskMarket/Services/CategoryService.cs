using DeskMarket.Data;
using DeskMarket.Models.Product;
using DeskMarket.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeskMarket.Services;

public class CategoryService(ApplicationDbContext categoryService): ICategoryService
{
    public async Task<IEnumerable<AddProductCategoryDropDownViewModel>> GetAllCategoriesForCreateAsync()
    {
        var allCategories = await categoryService
            .Categories
            .Select(x => new AddProductCategoryDropDownViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToListAsync();

        return allCategories;
    }

    public async Task<IEnumerable<EditProductCategoryDropDownViewModel>> GetAllCategoriesForEditAsync()
    {
        var allCategories = await categoryService.Categories
            .Select(x => new EditProductCategoryDropDownViewModel()
            {
                Id = x.Id,
                Name = x.Name,

            }).ToListAsync();

        return allCategories;
    }
}