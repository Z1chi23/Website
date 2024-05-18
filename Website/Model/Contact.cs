using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Contact
    {
        [Key] // Add this attribute to define the primary key
        public int Id { get; set; } // Assuming Id is the primary key

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}