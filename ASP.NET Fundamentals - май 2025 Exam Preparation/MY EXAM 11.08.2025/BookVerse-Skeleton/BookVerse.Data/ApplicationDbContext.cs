using BookVerse.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookVerse.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<IdentityUser>(options)
    {

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<UserBook> UsersBooks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

           var defaultUser = new IdentityUser
           {
               Id = "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
               UserName = "admin@bookverse.com",
               NormalizedUserName = "ADMIN@BOOKVERSE.COM",
               Email = "admin@bookverse.com",
               NormalizedEmail = "ADMIN@BOOKVERSE.COM",
               EmailConfirmed = true,
               PasswordHash = new PasswordHasher<IdentityUser>()
                   .HashPassword(new IdentityUser { UserName = "admin@bookverse.com" }, "Admin123!")
           };
           builder.Entity<IdentityUser>().HasData(defaultUser);

           builder.Entity<Genre>().HasData(
               new Genre { Id = 1, Name = "Fantasy" },
               new Genre { Id = 2, Name = "Thriller" },
               new Genre { Id = 3, Name = "Romance" },
               new Genre { Id = 4, Name = "Sci‑Fi" },
               new Genre { Id = 5, Name = "History" },
               new Genre { Id = 6, Name = "Non‑Fiction" }
           );

           builder.Entity<Book>().HasData(
               new Book
               {
                   Id = 1,
                   Title = "Whispers of the Mountain",
                   Description = "Emily Harper (released 2015): A quiet village, a hidden path, and a choice that changes everything.",
                   CoverImageUrl = "https://m.media-amazon.com/images/I/9187Qn8bL6L._UF1000,1000_QL80_.jpg",
                   PublisherId = defaultUser.Id,
                   PublishedOn = DateTime.Now,
                   GenreId = 1,
                   IsDeleted = false
               },
               new Book
               {
                   Id = 2,
                   Title = "Shadows in the Fog",
                   Description = "Michael Turner (released: 2017): An investigator follows a trail of secrets through a city shrouded in mystery.",
                   CoverImageUrl = "https://m.media-amazon.com/images/I/719g0mh9f2L._UF1000,1000_QL80_.jpg",
                   PublisherId = defaultUser.Id,
                   PublishedOn = DateTime.Now,
                   GenreId = 2,
                   IsDeleted = false
               },
               new Book
               {
                   Id = 3,
                   Title = "Letters from the Heart",
                   Description = "Sarah Collins (released 2020): A touching story about love, distance, and the power of written words.",
                   CoverImageUrl = "https://m.media-amazon.com/images/I/71zwodP9GzL._UF1000,1000_QL80_.jpg",
                   PublisherId = defaultUser.Id,
                   PublishedOn = DateTime.Now,
                   GenreId = 3,
                   IsDeleted = false
               }
           );
        }
    }
}
