using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website.Data;

namespace ShoppingCart.Infrastructure.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly AppDBContext _context;

        public CategoriesViewComponent(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync() => View(await _context.Categories.ToListAsync());
    }
}