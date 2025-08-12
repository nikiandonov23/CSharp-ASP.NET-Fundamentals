using CinemaApp.Data.Models;
using System.Reflection;

namespace CinemaApp.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class CinemaAppMay2025DbContext(DbContextOptions<CinemaAppMay2025DbContext> options) 
        : IdentityDbContext(options)
    {
       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<ApplicationUserMovie> ApplicationUserMovies { get; set; }= null!;

    }
}
