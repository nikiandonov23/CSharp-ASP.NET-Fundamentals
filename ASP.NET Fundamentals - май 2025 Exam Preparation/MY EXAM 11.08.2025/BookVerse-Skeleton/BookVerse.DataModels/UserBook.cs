using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookVerse.DataModels;
[PrimaryKey(nameof(UserId), nameof(BookId))]
public class UserBook
{

    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;
    public IdentityUser User { get; set; } = null!;


    [ForeignKey(nameof(Book))]
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    //	Has UserId – string, PrimaryKey, foreign key(required)
    //  •	Has User – IdentityUser
    //	Has BookId – integer, PrimaryKey, foreign key(required)
    //  •	Has Book – Book

}