using GameZone.Models.Game;

namespace GameZone.Services.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<AddGameGenreDropDownVIewModel>> GetAllGenresForAddAsync();

    Task<IEnumerable<EditGameGenreDropDownViewModel>> GetAllGenresForEditAsync();

    // Task<IEnumerable<EditRecipeCategoriesDropDownViewModel>> GetAllCategoriesForEditAsync();

    //service for DropDown

    //	 public class CategoryService(ApplicationDbContext context):ICategoryService
    //{
    //    public async Task<IEnumerable<CreateRecipeCategoryDropDownModel>> GetAllCategoriesForCreateAsync()
    //    {
    //        var allCategories = await context
    //            .Categories
    //            .Select(x => new CreateRecipeCategoryDropDownModel()
    //            {
    //                Id = x.Id,
    //                Name = x.Name,
    //
    //
    //            }).ToListAsync();
    //
    //        return allCategories;
    //    }
    //
    //    public async Task<IEnumerable<EditRecipeCategoriesDropDownViewModel>> GetAllCategoriesForEditAsync()
    //    {
    //        var categoriesForEdit = await context
    //            .Categories
    //            .Select(x => new EditRecipeCategoriesDropDownViewModel()
    //            {
    //                Id = x.Id,
    //                Name = x.Name,
    //            }).ToListAsync();
    //        return categoriesForEdit;
    //    }
    //}
}