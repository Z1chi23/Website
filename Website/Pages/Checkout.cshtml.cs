using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
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
        }

        public void OnGet()
        {
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