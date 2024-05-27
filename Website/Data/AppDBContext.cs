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
        public DbSet<SalesHistory> SalesHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configuration for ApplicationUser
            modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

            // Define the relationship between Product and Category entities
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .IsRequired();

            // Specify SQL Server column type for TotalPrice property in SalesHistory entity
            modelBuilder.Entity<SalesHistory>()
                .Property(e => e.TotalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SalesHistory>()
                .HasOne(sh => sh.Product)
                .WithMany()
                .HasForeignKey(sh => sh.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

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