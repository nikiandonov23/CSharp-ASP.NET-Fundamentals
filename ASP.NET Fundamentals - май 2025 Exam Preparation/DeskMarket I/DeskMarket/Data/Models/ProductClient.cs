using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeskMarket.Data.Models;


[PrimaryKey(nameof(ProductId), nameof(ClientId))]
public class ProductClient
{

    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;



    [ForeignKey(nameof(IdentityUser))]
    public string ClientId { get; set; } = null!;

    public IdentityUser IdentityUser { get; set; }=null!;
    //•	Has ProductId – integer, PrimaryKey, foreign key(required)
    //    •	Has Product – Product
    //•	Has ClientId – string, PrimaryKey, foreign key(required)
    //    •	Has Client – IdentityUser



}