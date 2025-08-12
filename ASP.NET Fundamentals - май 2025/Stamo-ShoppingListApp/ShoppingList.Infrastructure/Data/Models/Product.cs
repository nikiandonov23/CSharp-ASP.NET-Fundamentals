using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ShoppingList.Infrastructure.Data.Models
{
    [Comment("Shopping List Product")]
    public class Product
    {
        [Key]
        [Comment("Product Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;



        [MaxLength(500)]
        public string? Description { get; set; }



    }
}
