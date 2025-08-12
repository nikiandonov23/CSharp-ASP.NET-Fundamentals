using BookVerse.DataModels;
using BookVerse.Services.Core.Contracts;
using BookVerse.ViewModels.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookVerse.Web.Controllers
{


    //СЛАГАМ [Authorize] НА ЦЕЛИЯ КЛАС ЗА ДА НЕ ПРАВЯ UserId==null ПРОВЕРКИ

    [Authorize]
    public class BookController(IBookService bookServices, IGenreService genreServices) : BaseController
    {

      


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //userId може да е null ,тогава IsSaved и IsAuthor ще са false

            var userId = GetUserId();


            var allBooks = await bookServices.GetAllBooksAsync(userId);

            return View(allBooks);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = GetUserId();


            var createModel = new CreateBookViewModel()
            {
                Genres = await genreServices.GetAllGenresForCreateAsync(),
                PublishedOn = DateTime.Now.Date

            };

            return View(createModel);

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookViewModel inputModel)
        {
            var userId = GetUserId();
            //Tuk валидации за !ModelState.IsValid 
            if (!ModelState.IsValid || userId == null)
            {
                inputModel.Genres = await genreServices.GetAllGenresForCreateAsync();
                return View(inputModel);
            }

            var isItSuccess = await bookServices.CreateBookAsync(userId, inputModel);

            if (isItSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            inputModel.Genres = await genreServices.GetAllGenresForCreateAsync();
            return View(inputModel);
        }



        [HttpGet]
        public async Task<IActionResult> MyBooks()
        {
            var userId = GetUserId();


            //UserId не може да е null, защото е [Authorize] на целия клас
            var myBooks = await bookServices.GetMyBooksAsync(userId);

            return View(myBooks);  //или книги или празна колекция ако няма книги

        }

        [HttpPost]
        public async Task<IActionResult> AddToMyBooks(int id)  //tuka ne moga da gi opravq
        {
            var userId = GetUserId();



            //Не може да е null, защото е [Authorize] на целия клас
            var isSuccess = await bookServices.AddBookToMyBooksAsync(userId, id);

            if (isSuccess)
            {
                var referer = Request.Headers["Referer"].ToString();


                // Ако URL съдържа Details връщам kъм Details на книгата
                if (referer.Contains("/Book/Details", StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("Details", new { id });
                }
                // Ако съдържа AllBooks връщам към AllBooks 
                else if (referer.Contains("/Book/AllBooks", StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("MyBooks");
                }

            }

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var userId = GetUserId();

            //Не може да е null, защото е [Authorize] на целия клас
            var isSuccess = await bookServices.RemoveBookFromMyBooksAsync(userId, id);
            if (isSuccess)
            {
                return RedirectToAction(nameof(MyBooks));
            }
            //If it successfully removed from favorites redirect to favorites
            //return RedirectToAction(nameof(Cart/Favorites));

            return RedirectToAction(nameof(Index));

        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {


            var userId = GetUserId();

            var bookDetails = await bookServices.GetBookDetailsAsync(id, userId);
            if (bookDetails==null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(bookDetails);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();


            var modelToDelete = await bookServices.GetDeleteBookViewModelAsync(id, userId);
            if (modelToDelete == null)
            {
                return RedirectToAction(nameof(Index));
            }


            return View(modelToDelete);

        }


        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(DeleteBookViewModel inputModel)
        {
            var userId = GetUserId();
            //Тук ModelState.IsValid щото приемам модел и е [HttpPost] !!!
            if ( userId == null)
            {
                return RedirectToAction(nameof(ConfirmDelete));
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(ConfirmDelete));
            }

            var isItSuccess = await bookServices.DeleteBookAsync(inputModel, userId);
        
            if (!isItSuccess)
            {
                return RedirectToAction(nameof(ConfirmDelete));
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

            //Не може да е null, защото е [Authorize] на целия клас
            var bookToEdit = await bookServices.GetEditBookViewModelAsync(userId, id);
            if (bookToEdit != null)
            {
                bookToEdit.Genres = await genreServices.GetAllGenresForEditAsync();
                return View(bookToEdit);
            }

            if (bookToEdit == null)
            {
                return RedirectToAction(nameof(Index));
            }

            //Не може да е null защото UserId не може да е нулл
            bookToEdit.Genres = await genreServices.GetAllGenresForEditAsync();
          

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBookViewModel inputModel)
        {
            var userId = GetUserId();

            if (!ModelState.IsValid || userId == null)
            {
                //Ако ModelState не хареса някои от параметрите,презареждам дропдауна
                inputModel.Genres = await genreServices.GetAllGenresForEditAsync();
                return View(inputModel);
            }

            var isItSuccess = await bookServices.EditBookAsync(inputModel, userId);

            if (isItSuccess)
            {
                //Ако Edit-на successfully препращам към Details на едитнатия модел
                return RedirectToAction(nameof(Details), new { id = inputModel.Id });
            }

            //Ако Edit не е успешен презареждам дропдауна и показвам същия модел
            inputModel.Genres = await genreServices.GetAllGenresForEditAsync();
            return View(inputModel);

        }





    }

}

