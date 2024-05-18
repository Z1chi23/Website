using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Website.Model;
using Website.Models;

namespace Website.Data
{
    public class AppDBContext : IdentityDbContext<IdentityUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        // DbSet for Category
        public DbSet<Category> Category { get; set; }

        // DbSet for Contact
        public DbSet<Contact> Contacts { get; set; }

        // Identity-related DbSets are already included from IdentityDbContext
    }
}