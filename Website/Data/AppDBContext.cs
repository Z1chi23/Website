using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Website.Areas.Identity.Data;
using Website.Model;
using Website.Models;

namespace Website.Data
{
    public partial class AppDBContext : IdentityDbContext<IdentityUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<CartItem> CartItems { get; set; } // Add DbSet for CartItem

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

            // Define the relationship between Product and Category entities
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .IsRequired();

            // Configuration for CartItem entity
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(ci => ci.ProductId); // Assuming ProductId is the primary key
                entity.Property(ci => ci.ProductName).IsRequired().HasMaxLength(255);
                entity.Property(ci => ci.Price).IsRequired().HasColumnType("decimal(18, 2)"); // Specify SQL server column type for Price
                entity.Property(ci => ci.Quantity).IsRequired();
                entity.Property(ci => ci.Image).IsRequired().HasMaxLength(255);
            });
        }
    }

    internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(255);
            builder.Property(u => u.LastName).HasMaxLength(255);
        }
    }
}