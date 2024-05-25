using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Website.Data;
using Website.Model;

namespace Website.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(AppDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        [Required]
        public IFormFile Image { get; set; }

        public SelectList CategoryOptions { get; set; }

        public void OnGet(int id)
        {
            Product = _context.Products.Find(id);
            CategoryOptions = new SelectList(_context.Categories, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CategoryOptions = new SelectList(_context.Categories, "Id", "Name");
                return Page();
            }

            var fileName = Path.GetFileName(Image.FileName);
            var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }

            Product.image = "/uploads/" + fileName;
            _context.Products.Update(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("Admin");
        }
    }
}