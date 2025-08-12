using DeskMarket.Data;
using DeskMarket.Data.Models;
using DeskMarket.Models.Product;
using DeskMarket.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeskMarket.Services;

public class ProductService(ApplicationDbContext productService) : IProductService
{
    public async Task<IEnumerable<IndexProductViewModel>> GetAllProductsAsync(string? currentUserId)
    {
        var allProducts = await productService
            .Products
            .Where(x => x.IsDeleted == false)
             .Select(x => new IndexProductViewModel
             {
                 Id = x.Id,
                 ImageUrl = x.ImageUrl,
                 Price = x.Price,
                 ProductName = x.ProductName,
                 HasBought = x.ProductsClients.Any(x => x.ClientId == currentUserId),
                 IsSeller = x.SellerId.Equals(currentUserId)

             }).ToListAsync();
        return allProducts;
    }

    public async Task<bool> AddProductAsync(string userId, AddProductViewModel inputModel)
    {
        var isUserValid = await productService.Users
            .AnyAsync(x => x.Id == userId);

        var isCategoryValid = await productService.Categories
            .AnyAsync(x => x.Id == inputModel.CategoryId);

        if (!isUserValid || !isCategoryValid)
        {
            return false;
        }


        var productToBeAdded = new Product()
        {

            ProductName = inputModel.ProductName,
            Description = inputModel.Description,
            Price = inputModel.Price,
            ImageUrl = inputModel.ImageUrl,
            AddedOn = inputModel.AddedOn.Date,
            CategoryId = inputModel.CategoryId,
            SellerId = userId
        };

        await productService.Products.AddAsync(productToBeAdded);
        await productService.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<CartProductsViewModel>?> GetCartProductsAsync(string userId)
    {
        var isUserValid = await productService.Users
            .AnyAsync(x => x.Id == userId);
        if (!isUserValid)
        {
            return null;
        }

        var cartProducts = await productService
            .ProductsClients
            .Where(x => x.ClientId == userId)
            .Select(x => new CartProductsViewModel()
            {
                Id = x.ProductId,
                ImageUrl = x.Product.ImageUrl,
                Price = x.Product.Price,
                ProductName = x.Product.ProductName,
            }).ToListAsync();


        return cartProducts;
    }

    public async Task<bool> AddToCartAsync(string userId, int productId)
    {
        var isUserIdValid = await productService.Users
            .AnyAsync(x => x.Id == userId);

        var isProductValid = await productService.Products
            .AnyAsync(x => x.Id == productId);

        var isProductAlreadyAdded = await productService.ProductsClients
            .AnyAsync(x => x.ClientId == userId && x.ProductId == productId);


        if (!isProductValid || !isUserIdValid || isProductAlreadyAdded)
        {
            return false;
        }

        var productToAdd = new ProductClient()
        {
            ClientId = userId,
            ProductId = productId,
        };

        await productService.ProductsClients
            .AddAsync(productToAdd);
        await productService.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveProductFromCartAsync(string userId, int productId)
    {

        var isUserIdValid = await productService.Users
            .AnyAsync(x => x.Id == userId);

        var isProductValid = await productService.Products
            .AnyAsync(x => x.Id == productId);


        if (!isUserIdValid || !isProductValid)
        {
            return false;
        }

        await productService.ProductsClients
            .Where(x => x.ClientId == userId && x.ProductId == productId)
            .ExecuteDeleteAsync();
        await productService.SaveChangesAsync();
        return true;
    }

    public async Task<DetailsProductViewModel?> GetProductDetailsAsync(int productId, string? userId)
    {

        var isProductIdValid = await productService.Products
            .AnyAsync(x => x.Id == productId);

        if (!isProductIdValid)
        {
            return null;
        }



        var productDetails = await productService
            .Products
            .Where(x => x.Id == productId &&x.IsDeleted==false)
            .Select(x => new DetailsProductViewModel()
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                AddedOn = x.AddedOn.Date,
                CategoryName = x.Category.Name,
                Description = x.Description,
                Price = x.Price,
                ProductName = x.ProductName,
                Seller = x.Seller.Email ?? string.Empty

            }).SingleOrDefaultAsync();

        if (productDetails == null)
        {
            return null;
        }

        return productDetails;
    }

    public async Task<DeleteProductViewModel?> GetDeleteProductViewModelAsync(int productId, string? userId)
    {


        var isUserValid = await productService
            .Users
            .AnyAsync(x => x.Id == userId);

        var isProductIdValid = await productService
            .Products
            .AnyAsync(x => x.Id == productId && x.SellerId == userId);

        if (!isProductIdValid || !isUserValid)
        {
            return null;
        }


        var productToDelete = await productService
            .Products
            .Where(x => x.IsDeleted == false && x.Id == productId & x.SellerId == userId)
            .Select(x => new DeleteProductViewModel()
            {
                Id = x.Id,
                SellerId = x.SellerId,
                ProductName = x.ProductName,
                Seller = x.Seller.Email ?? string.Empty,

            }).SingleOrDefaultAsync();
        if (productToDelete == null)
        {
            return null;
        }
        return productToDelete;
    }

    public async Task<bool> DeleteProductAsync(DeleteProductViewModel inputModel, string? userId)
    {
        var isUserValid = await productService
            .Users
            .AnyAsync(x => x.Id == userId);

        var isInputModelValid = await productService
            .Products
            .AnyAsync(x => x.Id == inputModel.Id && x.SellerId == userId && x.IsDeleted == false);
        if (!isUserValid || !isInputModelValid)
        {
            return false;
        }



        var productToDelete = await productService
              .Products
              .Where(x => x.Id == inputModel.Id && x.SellerId == userId)
              .SingleOrDefaultAsync();


        if (productToDelete == null)
        {
            return false;
        }


        productToDelete.IsDeleted = true;
        await productService.SaveChangesAsync();
        return true;
    }

    public async Task<EditProductViewModel?> GetEditProductViewModelAsync(string userId, int productId)
    {

        var isUserValid = await productService
            .Users
            .AnyAsync(x => x.Id == userId);

        var isProductIdValid = await productService
            .Products
            .AnyAsync(x => x.Id == productId);

        if (!isUserValid || !isProductIdValid)
        {
            return null!;
        }




        var productToEdit = await productService
            .Products
            .Where(x => x.Id == productId && x.SellerId == userId)
            .Select(x => new EditProductViewModel()
            {
                AddedOn = x.AddedOn.Date,
                CategoryId = x.CategoryId,
                Description = x.Description,
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                ProductName = x.ProductName,
                SellerId = x.SellerId

            }).SingleOrDefaultAsync();

        return productToEdit;
    }

    public async Task<bool> EditProductAsync(EditProductViewModel inputModel, string userId)
    {

        var isUserValid = await productService
            .Users
            .AnyAsync(x => x.Id == userId);


        var isProductIdValid = await productService
            .Products
            .AnyAsync(x => x.Id == inputModel.Id && x.SellerId == userId);

        if (!isUserValid || !isProductIdValid)
        {
            return false;
        }




        var productToEdit = await productService
            .Products
            .Where(x => x.Id == inputModel.Id && x.SellerId == userId)
            .SingleOrDefaultAsync();

        if (productToEdit == null)
        {
            return false;
        }
        productToEdit.ProductName = inputModel.ProductName;
        productToEdit.Description = inputModel.Description;
        productToEdit.Price = inputModel.Price;
        productToEdit.ImageUrl = inputModel.ImageUrl;
        productToEdit.AddedOn = inputModel.AddedOn.Date;
        productToEdit.CategoryId = inputModel.CategoryId;

        await productService.SaveChangesAsync();
        return true;
    }
}