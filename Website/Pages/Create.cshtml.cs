using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Website.Data;
using Website.Model;
using System.Threading.Tasks;

namespace Website.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly AppDBContext _context;

        public CreateModel(AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Admin");
        }
    }
}