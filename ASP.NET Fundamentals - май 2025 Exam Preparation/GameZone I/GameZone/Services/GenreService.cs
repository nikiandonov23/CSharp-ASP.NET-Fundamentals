using GameZone.Data;
using GameZone.Models.Game;
using GameZone.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services;

public class GenreService(ApplicationDbContext genreService): IGenreService
{
    public async Task<IEnumerable<AddGameGenreDropDownVIewModel>> GetAllGenresForAddAsync()
    {
        var genresToReturn = await genreService
            .Genres
            .Select(x => new AddGameGenreDropDownVIewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

        return genresToReturn;
    }

    public async Task<IEnumerable<EditGameGenreDropDownViewModel>> GetAllGenresForEditAsync()
    {
        var allGenres=await genreService.Genres
            .Select(x => new EditGameGenreDropDownViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

        return allGenres;
    }
}