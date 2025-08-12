using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }




        [Required]
        public string Name { get; set; } = null!;


        public List<ProductNote> P:ro { get; set; }
    }
}
