using Horizons.Web.ViewModels.Destination;

namespace Horizons.Services.Core.Contracts;

public interface ITerrainService
{
    Task<IEnumerable<AddDestinationTerrainDropDownViewModel>> GetAllTerrainsForAddAsync();
    Task<IEnumerable<EditDestinationTerrainDropDownViewModel>> GetAllTerrainsForEditAsync();



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