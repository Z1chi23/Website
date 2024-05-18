using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Website.Data;
using Website.Model;

namespace Website.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly AppDBContext _db;

        public IEnumerable<Category> Categories { get; set; }

        public IndexModel(AppDBContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Categories = _db.Category;
        }
    }
}

