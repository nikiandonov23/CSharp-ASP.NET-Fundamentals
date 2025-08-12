using DeskMarket.Models.Product;
using DeskMarket.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskMarket.Controllers
{



    public class ProductController(IProductService productService, ICategoryService categoryService) : BaseController
    {

        //Ако някъде искам да редиректвам към ID
        //   return RedirectToAction(nameof(Details), new { id = inputModel.Id });


        //Ако някъде искам да рефрешна страница щото ми се губят дропдаун менютата

        // if (!ModelState.IsValid)
        //     {
        //         inputModel.Categories = await categoryServices.GetAllCategoriesForCreateAsync();
        //
        //         return View(inputModel);
        //     }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();

            var productsToShow = await productService.GetAllProductsAsync(userId);
            return View(productsToShow);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var createModel = new AddProductViewModel()
            {
                AddedOn = DateTime.Now.Date,
                Categories = await categoryService.GetAllCategoriesForCreateAsync(),


            };


            return View(createModel);

        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel inputModel)
        {
            var userId = GetUserId();
            //Tuk валидации за !ModelState.IsValid и userId==null
            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                inputModel.Categories = await categoryService.GetAllCategoriesForCreateAsync();
                return View(inputModel);
            }
            var isAdded = await productService.AddProductAsync(userId, inputModel);
            if (!isAdded)
            {
                inputModel.Categories = await categoryService.GetAllCategoriesForCreateAsync();
                return View(inputModel);
            }

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var cartProducts = await productService.GetCartProductsAsync(userId);
            if (cartProducts == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(cartProducts);
        }




        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var isItSuccess = await productService.AddToCartAsync(userId, id);

            if (!isItSuccess)
            {
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var isItSuccess = await productService.RemoveProductFromCartAsync(userId, id);
            if (!isItSuccess)
            {
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Cart));
        }




        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserId();


            var productDetails = await productService.GetProductDetailsAsync(id, userId);
            if (productDetails == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(productDetails);
        }

         [HttpGet]
         public async Task<IActionResult> Delete(int id)
         {
             var userId = GetUserId();
             if (userId==null)
             {
                return RedirectToAction(nameof(Index));

            }

            var productToDelete = await productService.GetDeleteProductViewModelAsync(id, userId);
             if (productToDelete==null)
             {
                 return RedirectToAction(nameof(Index));
             }

             return View(productToDelete);
         }
        
         [HttpPost]
         public async Task<IActionResult> Delete(DeleteProductViewModel inputModel)
         {
             var userId = GetUserId();



             var isItSuccess =await productService.DeleteProductAsync(inputModel, userId);
             if (!isItSuccess || !ModelState.IsValid)
             {
                 return RedirectToAction(nameof(Details));
             }

             return RedirectToAction(nameof(Index));
         }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction(nameof(Index));

            }

            var productToEdit = await productService.GetEditProductViewModelAsync(userId, id);




            if (productToEdit != null)
            {
                productToEdit.Categories = await categoryService.GetAllCategoriesForEditAsync();
                return View(productToEdit);
            }


            return RedirectToAction(nameof(Index));


        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel inputModel)
        {

            var userId = GetUserId();
            if (userId == null || !ModelState.IsValid)
            {
                inputModel.Categories = await categoryService.GetAllCategoriesForEditAsync();
                return View(inputModel);
            }

            var isItSuccess = await productService.EditProductAsync(inputModel, userId);
            if (!isItSuccess)
            {
                inputModel.Categories = await categoryService.GetAllCategoriesForEditAsync();
                return View(inputModel);
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
