using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Website.Data;
using Website.Model;

namespace Website.Pages
{
    public class ShopModel : PageModel
    {
        private readonly AppDBContext _context;

        public ShopModel(AppDBContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; }

        public List<Product> ShoppingCart
        {
            get
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("ShoppingCart");
                return cart ?? new List<Product>();
            }
            set
            {
                HttpContext.Session.SetObjectAsJson("ShoppingCart", value);
            }
        }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        public void OnGet()
        {
            Products = string.IsNullOrEmpty(SearchQuery)
                ? _context.Products.ToList()
                : _context.Products.Where(p => p.Name.Contains(SearchQuery)).ToList();
        }

        public IActionResult OnPostAddToCart(int productId, int quantity)
        {
            var product = _context.Products.Find(productId);

            if (product != null)
            {
                var cart = ShoppingCart;
                for (int i = 0; i < quantity; i++) // Add the product based on the specified quantity
                {
                    cart.Add(product);
                }
                ShoppingCart = cart;
            }

            return RedirectToPage();
        }

        public IActionResult OnPostRemoveFromCart(int productId)
        {
            var cart = ShoppingCart;
            var itemToRemove = cart.SingleOrDefault(item => item.Id == productId);

            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                ShoppingCart = cart;
            }

            return RedirectToPage();
        }
    }
}