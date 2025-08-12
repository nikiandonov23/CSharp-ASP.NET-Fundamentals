

using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using static CinemaApp.GCommon.ApplicationConstants;
namespace CinemaApp.Services.Core;

public class MovieService(CinemaAppMay2025DbContext context) : IMovieService
{
    public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync()
    {
        var allMovies = await context.Movies
            .AsNoTracking()
            .Where(x=>x.IsDeleted==false)
            .Select(x => new AllMoviesIndexViewModel()
            {
                Director = x.Director,
                Genre = x.Genre,
                Id = x.Id.ToString(),
                ImageUrl = x.ImageUrl,
                ReleaseDate = x.ReleaseDate.ToString(AppDateFormat),
                Title = x.Title
            }).ToListAsync();
          

        foreach (var movie in allMovies)
        {
            if (movie.ImageUrl.IsNullOrEmpty())
            {

                movie.ImageUrl = "/images/no-image.jpg"; // ✅ валиден URL за браузъра

            }
        }

        return allMovies;
    }

    public async Task AddAsync(MovieFormViewModel viewModel)
    {
        Movie newMovie = new Movie()
        {
            Genre = viewModel.Genre,
            Description = viewModel.Description,
            Director = viewModel.Director,
            Duration = viewModel.Duration,
            ImageUrl = viewModel.ImageUrl,
            Title = viewModel.Title,
            ReleaseDate = DateTime
                .ParseExact(viewModel.ReleaseDate, AppDateFormat, CultureInfo.InvariantCulture)

        };



        await context.AddAsync(newMovie);
        await context.SaveChangesAsync();

    }

    public async Task<MovieDetailsViewModel?> GetMovieDetailsByIdAsync(string? id)
    {
        if (!Guid.TryParse(id, out var movieGuid))
        {
            return null;
        }

        var movie = await context.Movies
            .AsNoTracking()
            .Where(x => x.IsDeleted == false && x.Id == movieGuid)
            .Select(x => new MovieDetailsViewModel
            {
                Id = x.Id.ToString(),
                Title = x.Title,
                Genre = x.Genre,
                Director = x.Director,
                Description = x.Description,
                Duration = x.Duration,
                ReleaseDate = x.ReleaseDate.ToString("yyyy-MM-dd"),
                ImageUrl = x.ImageUrl ?? "/images/no-image.jpg"
            })
            .FirstOrDefaultAsync();

        return movie;
    }

    public async Task<MovieFormViewModel?> GetForEditByIdAsync(string? id)
    {
        if (!Guid.TryParse(id, out var movieGuid))
        {
            return null;
        }

        var movie = await context.Movies
            .Where(x => x.Id == movieGuid)
            .Select(x => new MovieFormViewModel
            {
                Id = x.Id.ToString(),
                Description = x.Description,
                Director = x.Director,
                Duration = x.Duration,
                Genre = x.Genre,
                ImageUrl = x.ImageUrl,
                ReleaseDate = x.ReleaseDate.ToString("yyyy-MM-dd"),
                Title = x.Title
            })
            .FirstOrDefaultAsync();

        return movie;
    }

    public async Task EditAsync(string id, MovieFormViewModel movie)
    {



        var movieToEdit = await context.Movies
            .Where(x => x.Id.ToString() == id)
            .FirstOrDefaultAsync();


        if (movieToEdit == null)
        {
            return;
        }


        movieToEdit.Description = movie.Description;
        movieToEdit.Director = movie.Director;
        movieToEdit.Title = movie.Title;
        movieToEdit.Duration = movie.Duration;
        movieToEdit.Genre = movie.Genre;
        movieToEdit.ImageUrl = movie.ImageUrl;
        movieToEdit.ReleaseDate = DateTime.ParseExact(movie.ReleaseDate,AppDateFormat,CultureInfo.InvariantCulture);


        await context.SaveChangesAsync();

    }

    //Софт делийта само сменя статуса на IsDeleted и такаа базата си го държи ама не го показва :D
    public async Task SoftDeleteAsync(string? id)
    {
        if (!Guid.TryParse(id, out var movieGuid))
        {
            // По избор: log или хвърли изключение
            return;
        }

        var movieToDelete = await context.Movies
            .FirstOrDefaultAsync(x => x.Id == movieGuid);

        if (movieToDelete != null && !movieToDelete.IsDeleted)
        {
            movieToDelete.IsDeleted = true;
            await context.SaveChangesAsync();
        }
    }


    //Хард делийта направо го бастисва от базата
    public async Task HardDeleteAsync(string? id)
    {
        var movieToDelete = await context.Movies
            .Where(x => x.Id.ToString() == id)
            .FirstOrDefaultAsync();

        if (movieToDelete != null)
        {
            context.Movies.Remove(movieToDelete); // Replace ExecuteDeleteAsync with Remove
            await context.SaveChangesAsync(); // Save changes to persist the deletion
        }
        
    }
}