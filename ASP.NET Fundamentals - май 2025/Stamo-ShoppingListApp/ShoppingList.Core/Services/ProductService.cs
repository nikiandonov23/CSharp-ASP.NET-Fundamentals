using Microsoft.EntityFrameworkCore;
using ShoppingList.Core.Contracts;
using ShoppingList.Core.Dto;
using ShoppingList.Infrastructure.Data;
using ShoppingList.Infrastructure.Data.Models;


namespace ShoppingList.Core.Services
{
    public class ProductService(ShoppingListDbContext context) : IProductService
    {

        public async Task AddProductAsync(ProductViewModel model)
        {
            Product productToBeAdded = new Product()
            {
                Name = model.Name,
                Description = model.Description
            };

            await context.AddAsync(productToBeAdded);
            await context.SaveChangesAsync();
        }


        public async Task DeleteProductAsync(int id)
        {
            await context.Products
                 .Where(x => x.Id == id)
                 .ExecuteDeleteAsync();
        }



        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            return await context.Products
                .AsNoTracking()
                .Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,


                }).ToListAsync();
        }

        public async Task<ProductViewModel> GetByIdAsync(int id)
        {
            return await context.Products
                .Where(x => x.Id == id)
                .Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,

                }).FirstAsync();


        }

        public async Task UpdateProductAsync(ProductViewModel model)
        {

            var productToBeChanged = await context.Products
                .FirstAsync(x => x.Id == model.Id);

            productToBeChanged.Description = model.Description;
            productToBeChanged.Name = model.Name;

             await context.SaveChangesAsync();
        }


    }
}
