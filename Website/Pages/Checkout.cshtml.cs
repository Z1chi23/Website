using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Website.Model;

namespace Website.Pages
{
    public class CheckoutModel : PageModel
    {
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

        public IActionResult OnPostAddQuantity(int productId)
        {
            var cart = ShoppingCart;
            var item = cart.Find(p => p.Id == productId);
            if (item != null)
            {
                item.Quantity++;
                ShoppingCart = cart;
            }
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveQuantity(int productId)
        {
            var cart = ShoppingCart;
            var item = cart.Find(p => p.Id == productId);
            if (item != null && item.Quantity > 1)
            {
                item.Quantity--;
                ShoppingCart = cart;
            }
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveFromCart(int productId)
        {
            var cart = ShoppingCart;
            var itemToRemove = cart.Find(item => item.Id == productId);

            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                ShoppingCart = cart;
            }

            return RedirectToPage();
        }

        public IActionResult OnPost()
        {
            // Handle the purchase logic here (e.g., saving order to the database)

            // Clear the shopping cart
            HttpContext.Session.Remove("ShoppingCart");

            // Redirect to a confirmation page or the homepage
            return RedirectToPage("/Index");
        }
    }
}