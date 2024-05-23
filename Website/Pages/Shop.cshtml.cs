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

        public List<CartItem> ShoppingCart
        {
            get
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("ShoppingCart");
                return cart ?? new List<CartItem>();
            }
            set
            {
                HttpContext.Session.SetObjectAsJson("ShoppingCart", value);
            }
        }

        public void OnGet()
        {
            Products = _context.Products.ToList();
        }

        public IActionResult OnPostAddToCart(int productId)
        {
            var product = _context.Products.Find(productId);

            if (product != null)
            {
                var cart = ShoppingCart;
                var cartItem = cart.SingleOrDefault(item => item.ProductId == productId);

                if (cartItem == null)
                {
                    cart.Add(new CartItem
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Price = product.Price,
                        Quantity = 1,
                        Image = product.image
                    });
                }
                else
                {
                    cartItem.Quantity++;
                }

                ShoppingCart = cart;
            }

            return RedirectToPage();
        }

        public IActionResult OnPostRemoveFromCart(int productId)
        {
            var cart = ShoppingCart;
            var itemToRemove = cart.SingleOrDefault(item => item.ProductId == productId);

            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
            }

            ShoppingCart = cart;

            return RedirectToPage();
        }
    }
}