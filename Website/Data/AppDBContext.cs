using Microsoft.EntityFrameworkCore;
using Website.Model;
using Website.Models;

namespace Website.Data
{
    public class AppDBContext : DbContext
    {
        // Constructor
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        // DbSet for Category
        public DbSet<Category> Category { get; set; }

        // DbSet for Contact
        public DbSet<Contact> Contacts { get; set; }
    }
}