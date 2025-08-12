using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Data.Models;


[PrimaryKey(nameof(UserId),nameof(DestinationId))]
public class UserDestination
{
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;
    public IdentityUser User { get; set; } = null!;




    [ForeignKey(nameof(Destination))]
    public int DestinationId { get; set; }
    public Destination Destination { get; set; } = null!;

}