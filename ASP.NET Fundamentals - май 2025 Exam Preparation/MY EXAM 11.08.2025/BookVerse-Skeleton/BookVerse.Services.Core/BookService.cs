using System.Globalization;
using BookVerse.Data;
using BookVerse.DataModels;
using BookVerse.Services.Core.Contracts;
using BookVerse.ViewModels.Book;
using Microsoft.EntityFrameworkCore;
using static BookVerse.GCommon.ValidationConstants;
namespace BookVerse.Services.Core
{
    public class BookService(ApplicationDbContext bookService) : IBookService
    {
        public async Task<IEnumerable<IndexBookViewModel>> GetAllBooksAsync(string? currentUserId)
        {
            var userId = currentUserId ?? string.Empty; //Ако currentUserId e null да не гръмне !!!

            var allBooks = await bookService.Books
                .Where(x => x.IsDeleted == false)
                .Select(x => new IndexBookViewModel()
                {
                    Id = x.Id,
                    Genre = x.Genre.Name,
                    CoverImageUrl = x.CoverImageUrl,
                    SavedCount = x.UsersBooks.Count,
                    Title = x.Title,
                    IsAuthor = x.PublisherId == userId,
                    IsSaved = x.UsersBooks.Any(x => x.UserId == userId)


                }).ToListAsync();

            return allBooks;
        }

        public async Task<bool> CreateBookAsync(string userId, CreateBookViewModel inputModel)
        {

            var isUserValid = await bookService.Users
                .AnyAsync(x => x.Id == userId);

            var isGenreValid = await bookService.Genres
                .AnyAsync(x => x.Id == inputModel.GenreId);

            if (!isUserValid || !isGenreValid)
            {
                return false; // Ако потребителят или жанрa не са валидни, връщам false
            }


            var newBook = new Book()
            {

                Title = inputModel.Title,
                Description = inputModel.Description,
                CoverImageUrl = inputModel.CoverImageUrl,
                GenreId = inputModel.GenreId,
                PublisherId = userId,
                PublishedOn = inputModel.PublishedOn,

            };

            await bookService.Books.AddAsync(newBook);
            await bookService.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MyBooksBookViewModel>> GetMyBooksAsync(string userId)
        {

            var isUserValid = await bookService.Users
                .AnyAsync(x => x.Id == userId);
            if (!isUserValid)
            {
                return null!; // Ако потребителят не е валиден, връщам null
            }

            var myBooks = await bookService
                .UsersBooks
                .Where(x => x.UserId == userId)
                .Select(x => new MyBooksBookViewModel()
                {

                    Id = x.Book.Id,
                    Title = x.Book.Title,
                    Genre = x.Book.Genre.Name,
                    CoverImageUrl = x.Book.CoverImageUrl

                }).ToListAsync();

            return myBooks;
        }

        public async Task<bool> AddBookToMyBooksAsync(string userId, int bookId)
        {
            var isUserValid = await bookService.Users
                .AsNoTracking()
                .AnyAsync(x => x.Id == userId);

            var isBookValid = await bookService.Books
                .AsNoTracking()
                .AnyAsync(x => x.Id == bookId && x.IsDeleted == false);

            var isBookAlreadyAdded = await bookService
                .UsersBooks
                .AsNoTracking()
                .AnyAsync(x => x.UserId == userId && x.BookId == bookId);

            if (!isUserValid || !isBookValid || isBookAlreadyAdded)
            {                   //Ако книгата е вече добавена
                return false; // Ако потребителят не е валиден,или книгата не е  връщам false
            }

            var bookToBeAdded = new UserBook()
            {
                BookId = bookId,
                UserId = userId
            };
            await bookService.UsersBooks.AddAsync(bookToBeAdded);
            await bookService.SaveChangesAsync();
            return true;

        }

        public async Task<bool> RemoveBookFromMyBooksAsync(string userId, int bookId)
        {
            var isUserValid = await bookService
                .Users
                .AsNoTracking()
                .AnyAsync(x => x.Id == userId);

            var isBookValid = await bookService
                .Books
                .AsNoTracking()
                .AnyAsync(x => x.Id == bookId && x.IsDeleted == false);

            if (!isUserValid || !isBookValid)
            {
                return false;
            }





            await bookService
                .UsersBooks
                .Where(x => x.UserId == userId && x.BookId == bookId)
                .ExecuteDeleteAsync();

            await bookService.SaveChangesAsync();

            return true;
        }

        public async Task<DetailsBookViewModel?> GetBookDetailsAsync(int bookId, string? userId)
        {

            var currentUserId = userId ?? string.Empty; //Ако currentUserId e null да не гръмне !!!


            var isBookValid = await bookService //true ako e valid
                .Books
                .AnyAsync(x => x.Id == bookId);
            if (!isBookValid)
            {
                return null; // Ако книгата не е валидна, връщам null
            }


            var bookDetails = await bookService.Books
            .Where(x => x.Id == bookId && x.IsDeleted == false)
            .Select(x => new DetailsBookViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CoverImageUrl = x.CoverImageUrl,
                Genre = x.Genre.Name,
                PublishedOn = x.PublishedOn.Date,
                Publisher = x.Publisher.Email ?? string.Empty,
                IsSaved = x.UsersBooks.Any(x => x.UserId == currentUserId),
                IsAuthor = x.PublisherId.Equals(currentUserId)


            }).SingleOrDefaultAsync();
            return bookDetails;
        }

        public async Task<DeleteBookViewModel?> GetDeleteBookViewModelAsync(int bookId, string? userId)
        {

            var isUserValid =await bookService
                .Users
                .AsNoTracking()
                .AnyAsync(x => x.Id == userId);

            var isBookValid =await bookService
                .Books
                .AsNoTracking()
                .AnyAsync(x => x.Id == bookId && x.PublisherId == userId && x.IsDeleted == false);

            if (!isBookValid || !isUserValid)
            {
                return null; // Ако книгата или потребителят не са валидни, връщам null
            }

            var deleteModel =await bookService
                .Books
                .Where(x => x.Id == bookId && x.PublisherId == userId && x.IsDeleted == false)
                .Select(x => new DeleteBookViewModel()
                {
                    Publisher = x.Publisher.Email ?? string.Empty,
                    Id = x.Id,
                    Title = x.Title,
                    
                    


                }).SingleOrDefaultAsync();

            return  deleteModel;
        }

        public async Task<bool> DeleteBookAsync(DeleteBookViewModel inputModel, string? userId)
        {

            var isUserValid = await bookService
                .Users
                .AnyAsync(x => x.Id == userId);

            var isBookValid = await bookService
                .Books
                .AsNoTracking()
                .AnyAsync(x => x.Id == inputModel.Id && x.PublisherId == userId && x.IsDeleted == false);



            if (!isUserValid || !isBookValid)
            {
                return false;  //Ako узър или книга са невалидни връщам фалсе
            }

            

            var bookToDelete=await bookService
                .Books
                .Where(x=>x.Id==inputModel.Id&&x.PublisherId==userId&&x.IsDeleted==false)
                .SingleOrDefaultAsync();
            if (bookToDelete == null)
            {
                return false;
            }


            bookToDelete.IsDeleted = true; // Маркирам книгата като изтрита



            await bookService.SaveChangesAsync(); // Записвам промените в базата данни
            return true; // Ако всичко е наред връщам true
        }

        public async Task<EditBookViewModel?> GetEditBookViewModelAsync(string userId, int bookId)
        {

            var isUserValid = await bookService.Users
                .AnyAsync(x => x.Id == userId);

            var isBookValid = await bookService //true ako e valid
                .Books
                .AnyAsync(x => x.Id == bookId);


            if (!isBookValid || !isUserValid)
            {
                return null;
            }



            var bookToEdit=await bookService
                .Books
                .Where(x => x.Id == bookId && x.PublisherId == userId && x.IsDeleted == false)
                .Select(x => new EditBookViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CoverImageUrl = x.CoverImageUrl,
                    GenreId = x.GenreId,
                    PublishedOn = x.PublishedOn.Date,
                    
                    
                }).SingleOrDefaultAsync();
            return bookToEdit;
        }

        public async Task<bool> EditBookAsync(EditBookViewModel inputModel, string userId)
        {
         


            var isUserValid = await bookService.Users
                .AnyAsync(x => x.Id == userId);

            var isBookValid = await bookService
                .Books
                .AnyAsync(x => x.Id == inputModel.Id && x.PublisherId == userId && x.IsDeleted == false);

            if (!isUserValid || !isBookValid)
            {
                return false;
            }


            var bookToEdit=await bookService
                .Books
                .Where(x=>x.Id==inputModel.Id&&x.PublisherId==userId&&x.IsDeleted==false)
                .SingleOrDefaultAsync();

            if (bookToEdit==null)
            {
                return false; // Ако книгата не e валидна връщам false
            }

            bookToEdit.Title = inputModel.Title;
            bookToEdit.Description = inputModel.Description;
            bookToEdit.CoverImageUrl = inputModel.CoverImageUrl;
            bookToEdit.GenreId = inputModel.GenreId;
            bookToEdit.PublishedOn = inputModel.PublishedOn.Date;


            await bookService.SaveChangesAsync();
            return true; // Ако всичко е наред, връщам true

        }
    }
}
