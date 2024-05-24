using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using Website.Data;
using Website.Model;

namespace Website.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly AppDBContext _context;

        public CheckoutModel(AppDBContext context)
        {
            _context = context;
        }

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
            foreach (var item in ShoppingCart)
            {
                var salesHistory = new SalesHistory
                {
                    ProductId = item.Id,
                    Quantity = item.Quantity,
                    TotalPrice = item.Price * item.Quantity,
                    SaleDate = DateTime.Now
                    // You may add more properties as needed
                };
                _context.SalesHistory.Add(salesHistory);
            }

            // Clear the shopping cart
            HttpContext.Session.Remove("ShoppingCart");

            // Save changes to the database
            _context.SaveChanges();

            // Redirect to a confirmation page or the homepage
            return RedirectToPage("/Index");
        }
    }
}