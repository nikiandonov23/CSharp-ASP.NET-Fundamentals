using BookVerse.Data;
using BookVerse.Services.Core.Contracts;
using BookVerse.ViewModels.Book;
using Microsoft.EntityFrameworkCore;

namespace BookVerse.Services.Core;

public class GenreService(ApplicationDbContext genreService) : IGenreService
{
    public async Task<IEnumerable<CreateBookGenreDropDownViewModel>> GetAllGenresForCreateAsync()
    {
        var allGenres =await genreService.Genres
            .Select(x => new CreateBookGenreDropDownViewModel()
            {

                Id = x.Id,
                Name = x.Name
            }).ToListAsync();


        return allGenres;
    }




    public async Task<IEnumerable<EditBookGenreDropDownViewModel>> GetAllGenresForEditAsync()
    {
        var allGenres = await genreService.Genres
            .Select(x => new EditBookGenreDropDownViewModel()
            {

                Id = x.Id,
                Name = x.Name
            }).ToListAsync();


        return allGenres;
    }
}