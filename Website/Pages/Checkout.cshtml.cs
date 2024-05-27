using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Website.Data;
using Website.Model;

namespace Website.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly AppDBContext _context;
        private readonly ILogger<CheckoutModel> _logger;

        public CheckoutModel(AppDBContext context, ILogger<CheckoutModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Product> ShoppingCart { get; set; }

        public IActionResult OnGet()
        {
            // Retrieve the shopping cart from session
            ShoppingCart = HttpContext.Session.GetObjectFromJson<List<Product>>("ShoppingCart") ?? new List<Product>();
            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                // Retrieve the shopping cart from session
                ShoppingCart = HttpContext.Session.GetObjectFromJson<List<Product>>("ShoppingCart") ?? new List<Product>();

                // Loop through each item in the shopping cart and add a sales history record
                foreach (var item in ShoppingCart)
                {
                    var salesHistory = new SalesHistory
                    {
                        ProductId = item.Id,
                        Quantity = 1, // Assuming quantity is 1 for each item
                        TotalPrice = item.Price, // Assuming total price is the same as the item price
                        SaleDate = DateTime.Now
                    };

                    // Add the new sales history record to the database context
                    _context.SalesHistories.Add(salesHistory);
                }

                // Save changes to the database
                _context.SaveChanges();

                // Clear the shopping cart from session
                HttpContext.Session.Remove("ShoppingCart");

                // Redirect to a confirmation page or the homepage
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during checkout");
                // Redirect to an error page
                return RedirectToPage("/Error");
            }
        }
    }
}