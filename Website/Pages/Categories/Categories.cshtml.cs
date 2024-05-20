using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Website.Data;
using Website.Model; // Ensure you have the correct namespace for your Category class

namespace Website.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly AppDBContext _context; // Replace YourDbContext with your actual DbContext class

        public IndexModel(AppDBContext context)
        {
            _context = context;
        }

        public List<Category> Categories { get; set; } // Ensure you have this property

        public void OnGet()
        {
            // Retrieve categories from the database and assign them to the Categories property
            Categories = _context.Categories.ToList(); // Replace Categories with your DbSet property
        }
    }
}