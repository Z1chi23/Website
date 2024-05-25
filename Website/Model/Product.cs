using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Model
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a value")]
        public string Name { get; set; }
        public string Slug { get; set; }

        [Required,MinLength(4, ErrorMessage = "Minimum Length is 2")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string image { get; set; }

        public int Quantity { get; set; }

        [NotMapped] // This property will not be mapped to the database
        public IFormFile Image { get; set; } // Use this property for file upload

    }
}
