using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Website.Data;
using Website.Model;

namespace Website.Pages.Admin
{
    public class AdminModel : PageModel
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdminModel(AppDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Product NewItem { get; set; }

        public IQueryable<Category> Categories { get; set; } // Add Categories property

        public async Task OnGetAsync()
        {
            Categories = _context.Categories; // Retrieve categories from the database
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Handle file upload
            if (NewItem.Image != null && NewItem.Image.Length > 0)
            {
                var fileName = Path.GetFileName(NewItem.Image.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, "HomeImage", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await NewItem.Image.CopyToAsync(stream);
                }

                NewItem.image = "" + fileName; // Use the 'image' property to store the image path
            }

            _context.Products.Add(NewItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Admin");
        }
    }
}