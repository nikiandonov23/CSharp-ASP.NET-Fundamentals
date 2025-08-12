using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Horizons.Data.Models;

public class UserDestination
{
    //	Has UserId – string, PrimaryKey, foreign key(required)
    //   •	Has User – IdentityUser
    //	Has DestinationId – integer, PrimaryKey, foreign key(required)
    //   •	Has Destination – Destination

    [ForeignKey(nameof(User))]
    [Required]
    public string UserId { get; set; } = null!;

    public virtual IdentityUser User { get; set; } = null!;



    [ForeignKey(nameof(Destination))]
    [Required]
    public int DestinationId { get; set; }

    public virtual Destination Destination { get; set; } = null!;
}