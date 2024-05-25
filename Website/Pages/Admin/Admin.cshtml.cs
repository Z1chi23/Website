using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Website.Data;
using Website.Model;

namespace Website.Pages.Admin
{
    public class AdminModel : PageModel
    {
        private readonly AppDBContext _context;

        public AdminModel(AppDBContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _context.Products.ToListAsync();
        }

        public async Task<IActionResult> OnGetEditAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Admin");
        }

        // Handler for editing product
        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            var productToUpdate = await _context.Products.FindAsync(id);
            if (productToUpdate == null)
            {
                return NotFound();
            }

            // Update the product properties based on form data
            if (await TryUpdateModelAsync<Product>(
                productToUpdate,
                "product",   // Prefix for form data
                p => p.Name, p => p.Price, p => p.Description))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Admin/Admin");
            }

            return Page();
        }

        // Handler for creating new product
        public async Task<IActionResult> OnPostCreateAsync(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Admin/Admin");
            }

            return Page();
        }
    }
}