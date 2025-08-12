using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Models
{
    [Comment("User watchlist Entry in the system")]
    public class ApplicationUserMovie
    {

        [Comment("Foreign Key to the referenced ApplicationUser.Part of the COMPOSITE KEY")]
        public string ApplicationUserId { get; set; } = null!;
        public virtual IdentityUser ApplicationUser { get; set; }=null!;




        [Comment("Foreign key to the referenced Movie.Part of the COMPOSITE KEY")]
        public Guid MovieId { get; set; }
        public virtual Movie Movie { get; set; } =null!;


        [Comment("Show if ApplicationUserMovie entry is deleted")]
        public bool IsDeleted { get; set; }

    }
}
