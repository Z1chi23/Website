using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        public IQueryable<Category> Categories { get; set; }
        public IQueryable<Product> Products { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Check if the admin is logged in
            if (HttpContext.Session.GetString("IsAdminLoggedIn") != "true")
            {
                return RedirectToPage("/Admin/AdminLogin");
            }

            // Load categories data
            Categories = _context.Categories;

            // Load products data
            Products = _context.Products.Include(p => p.Category);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Logic for adding a new item...
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (NewItem.Image != null && NewItem.Image.Length > 0)
            {
                var fileName = Path.GetFileName(NewItem.Image.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, "HomeImage", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await NewItem.Image.CopyToAsync(stream);
                }

                NewItem.image = fileName;
            }

            _context.Products.Add(NewItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Admin");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Logic for deleting an item...
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Admin");
        }

        public async Task<IActionResult> OnPostEditAsync(int id, [Bind("Id,Name,Description,Price,CategoryId")] Product product)
        {
            // Logic for editing an item...
            var productToUpdate = await _context.Products.FindAsync(id);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Product>(
                productToUpdate,
                "product",
                p => p.Name, p => p.Description, p => p.Price, p => p.CategoryId))
            {
                if (product.Image != null && product.Image.Length > 0)
                {
                    var fileName = Path.GetFileName(product.Image.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, "HomeImage", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.Image.CopyToAsync(stream);
                    }

                    productToUpdate.image = fileName;
                }

                await _context.SaveChangesAsync();
                return RedirectToPage("/Admin/Admin");
            }

            return Page();
        }
    }
}