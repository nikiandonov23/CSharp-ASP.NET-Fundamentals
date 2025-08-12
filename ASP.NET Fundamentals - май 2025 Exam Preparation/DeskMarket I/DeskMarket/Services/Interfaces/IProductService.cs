using DeskMarket.Models.Product;

namespace DeskMarket.Services.Interfaces;

public interface IProductService
{
    //Index GetAllModels
    public Task<IEnumerable<IndexProductViewModel>> GetAllProductsAsync(string? currentUserId);

    //Create /Add ;
    //No predi tova Nov InterfaceDropDown+ServiceDropDown za DROPDOWN podmodela
    //Nov [HttpGet] koito zarejda DropDownModela
    //public async Task<IActionResult> Create()
    //i vrushta View(GLAVNIQ MODE) sus zareden parametur PODMODELA.Koito se vika on podmodel servisa
     public Task<bool> AddProductAsync(string userId, AddProductViewModel inputModel);


    //GetAllCart
    public Task<IEnumerable<CartProductsViewModel>?> GetCartProductsAsync(string userId);

    //Add to AddToCart
    public Task<bool> AddToCartAsync(string userId, int productId);


    //Remove from Cart
    public Task<bool> RemoveProductFromCartAsync(string userId, int productId);


    //GetDetails
    public Task<DetailsProductViewModel?> GetProductDetailsAsync(int productId, string? userId);

    //GetDeleteViewModel
    public Task<DeleteProductViewModel?> GetDeleteProductViewModelAsync(int productId, string? userId);

    //Delete
    public Task<bool> DeleteProductAsync(DeleteProductViewModel inputModel, string? userId);




    //Edit  .Tuka pravq Edit s prazen dropdown model
    public Task<EditProductViewModel?> GetEditProductViewModelAsync(string userId, int productId);

    //Edit(SAVE)
    //tuka ve4e persistvam promenite SAVE
    //No predi tova Nov InterfaceDropDown+ServiceDropDown za DROPDOWN podmodela
    //Nov [HttpGet] koito zarejda DropDownModela
    //public async Task<IActionResult> Create()
    //i vrushta View(GLAVNIQ MODE) sus zareden parametur PODMODELA.Koito se vika on podmodel servisa
    public Task<bool> EditProductAsync(EditProductViewModel inputModel, string userId);
}