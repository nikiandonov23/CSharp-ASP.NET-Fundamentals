using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Core.Dto
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        [Display(Name = "Product Name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Field {0} must be between {2} and {1} characters")]
        public string Name { get; set; } = string.Empty;




        [Display(Name = "That is the product description")]
        [MaxLength(500, ErrorMessage = "Field {0} must be no longer than {1} characters")]
        public string? Description { get; set; }
    }
}
