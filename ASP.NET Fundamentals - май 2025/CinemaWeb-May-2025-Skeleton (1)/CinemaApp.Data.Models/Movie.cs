using CinemaApp.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Data.Models
{

    [Comment("Movie in the system")]
    public class Movie
    {

        [Comment("Movie Identifier")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();



        [Comment("Movie Title")]
        [Required(ErrorMessage = "Title is required !")]
        [StringLength(EntityConstants.TitleMaxLength, ErrorMessage = "Title cannot exceed 100 characters !")]
        public string Title { get; set; } = null!;


        [Comment("Movie Genre")]
        [Required(ErrorMessage = "Genre is required")]
        [StringLength(EntityConstants.GenreMaxLength, ErrorMessage = "Genre cannot exceed 50 characters")]
        public string Genre { get; set; } = null!;


        [Comment("Movie Release Date")]
        [Required(ErrorMessage = "Release date is required")]
        public DateTime ReleaseDate { get; set; }





        [Comment("Movie Director")]
        [StringLength(EntityConstants.DirectorNameMaxLength, ErrorMessage = "Director cannot exceed 100 characters")]
        [Required(ErrorMessage = "Director is required")]
        public string Director { get; set; } = null!;





        [Range(EntityConstants.DurationMin, EntityConstants.DurationMax)]
        public int Duration { get; set; }





        [MaxLength(EntityConstants.DescriptionMaxLength)]
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; } = false;



        public ICollection<ApplicationUserMovie> UserWatchLists { get; set; } =
            new HashSet<ApplicationUserMovie>();


    }
}
