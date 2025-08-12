using BookVerse.ViewModels.Book;

namespace BookVerse.Services.Core.Contracts
{
    public interface IBookService
    {

        //Index GetAllModels
        public Task<IEnumerable<IndexBookViewModel>> GetAllBooksAsync(string? currentUserId);

        //Create /Add ;
        public Task<bool> CreateBookAsync(string userId, CreateBookViewModel inputModel);


        //GetMyBooks (модела не не нулабъл щото ако няма нищо е просто празна колекция)
        public Task<IEnumerable<MyBooksBookViewModel>> GetMyBooksAsync(string userId);

        //Add to MyBooks
        public Task<bool> AddBookToMyBooksAsync(string userId, int bookId);


        //Remove from MyBooks
        public Task<bool> RemoveBookFromMyBooksAsync(string userId, int bookId);


        //GetDetails
        public Task<DetailsBookViewModel?> GetBookDetailsAsync(int bookId, string? userId);

        //GetDeleteViewModel
        public Task<DeleteBookViewModel?> GetDeleteBookViewModelAsync(int bookId, string? userId);

        //Delete
        public Task<bool> DeleteBookAsync(DeleteBookViewModel inputModel, string? userId);

        //Edit  
        public Task<EditBookViewModel?> GetEditBookViewModelAsync(string userId, int bookId);

        //Edit(SAVE)
       
        public Task<bool> EditBookAsync(EditBookViewModel inputModel, string userId);


    }
}
