using CinemaApp.Web.ViewModels.Movie;

namespace CinemaApp.Services.Core.Interfaces;

public interface IMovieService
{
    Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync();
    Task AddAsync(MovieFormViewModel viewModel);
    Task<MovieDetailsViewModel?> GetMovieDetailsByIdAsync(string id);



    Task<MovieFormViewModel?> GetForEditByIdAsync(string id);
    Task EditAsync(string id, MovieFormViewModel movie);

    Task SoftDeleteAsync(string? id);
    Task HardDeleteAsync(string? id);
}