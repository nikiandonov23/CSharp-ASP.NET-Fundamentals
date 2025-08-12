using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingListApp2025.Models
{
    public class ProductNote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = null!;





        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;


    }
}