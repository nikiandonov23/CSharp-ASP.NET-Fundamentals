using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingListApp.Models
{
    public class ProductNote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = null!;


     
        public int ProductId { get; set; }
        public Product Product { get; set; }


    }
}
