using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp2025.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }




        [Required]
        public string Name { get; set; } = null!;


        public virtual List<ProductNote> ProductNotes { get; set; } = new List<ProductNote>();
    }
}