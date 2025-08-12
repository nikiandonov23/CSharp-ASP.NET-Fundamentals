using BookVerse.ViewModels.Book;

namespace BookVerse.Services.Core.Contracts;

public interface IGenreService
{
    Task<IEnumerable<CreateBookGenreDropDownViewModel>> GetAllGenresForCreateAsync();

    Task<IEnumerable<EditBookGenreDropDownViewModel>> GetAllGenresForEditAsync();


}