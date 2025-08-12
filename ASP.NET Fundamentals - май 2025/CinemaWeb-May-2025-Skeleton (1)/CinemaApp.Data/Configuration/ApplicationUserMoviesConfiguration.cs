using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration;

public class ApplicationUserMoviesConfiguration: IEntityTypeConfiguration<ApplicationUserMovie>
{
    public void Configure(EntityTypeBuilder<ApplicationUserMovie> builder)
    {
        //Define composite key
        builder.HasKey(x => new { x.ApplicationUserId, x.MovieId });

        //Define required constraint for ApplicationUserId
        builder.Property(x => x.ApplicationUserId)
            .IsRequired();

        builder
            .HasOne(x => x.ApplicationUser)
            .WithMany()
            .HasForeignKey(x => x.ApplicationUserId);

        builder
            .HasOne(x => x.Movie)
            .WithMany(m=>m.UserWatchLists)
            .HasForeignKey(x => x.MovieId);






        builder
            .Property(x => x.IsDeleted)
            .HasDefaultValue(false);



        //QUERY FILTERS :
        //query filter .Връща само записите които са IsDeleted=false (за филма)
        builder
            .HasQueryFilter(x => x.Movie.IsDeleted == false);
        //query filter .Връща само записите които са IsDeleted=false (за watchlist-a)
        builder
            .HasQueryFilter(x => x.IsDeleted == false);

    }
}