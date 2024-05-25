using System.ComponentModel.DataAnnotations;

namespace Website.Model
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        public decimal Price { get; set; }

        // Foreign key to associate with Product
        public int ProductId { get; set; }

        // Navigation property for the associated Product
        public Product Product { get; set; }
    }
}