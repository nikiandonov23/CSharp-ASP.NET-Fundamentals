using RecipeSharingPlatform.ViewModels.Recipe;

namespace RecipeSharingPlatform.Services.Core.Contracts;

public interface ICategoryService
{
     Task<IEnumerable<CreateRecipeCategoryDropDownModel>> GetAllCategoriesForCreateAsync();

     Task<IEnumerable<EditRecipeCategoriesDropDownViewModel>> GetAllCategoriesForEditAsync();


}