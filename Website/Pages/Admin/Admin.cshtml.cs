using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public IList<Product> Items { get; set; } // Property to hold the list of items

        public async Task<IActionResult> OnGetAsync()
        {
            Items = await _context.Products.ToListAsync(); // Retrieve all items from the database
            return Page();
        }

        public async Task<IActionResult> OnGetDeleteAsync(int id)
        {
            var itemToDelete = await _context.Products.FindAsync(id);
            if (itemToDelete != null)
            {
                _context.Products.Remove(itemToDelete);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Admin/Admin");
        }

        public async Task<IActionResult> OnGetEditAsync(int id)
        {
            NewItem = await _context.Products.FindAsync(id);
            if (NewItem != null)
            {
                return Page();
            }
            return RedirectToPage("/Admin/Admin");
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(NewItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Admin");
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
                var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await NewItem.Image.CopyToAsync(stream);
                }

                NewItem.image = "/uploads/" + fileName; // Use the 'image' property to store the image path
            }

            _context.Products.Add(NewItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Admin");
        }
    }
}