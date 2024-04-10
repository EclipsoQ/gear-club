using GearClub.Areas.Identity.Data;
using GearClub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GearClub.Data;

public class GearClubContext : IdentityDbContext<ApplicationUser>
{
    public GearClubContext(DbContextOptions<GearClubContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }       
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Category_Product> Category_Products { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartDetail> CartDetails { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Specification> Specifications { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<Address>()
            .HasOne(c => c.User)
            .WithMany(c => c.Addresses)
            .HasForeignKey(c => c.UserId);

        builder.Entity<Category_Product>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Category_Products)
            .HasForeignKey(c => c.CategoryId);
            
        builder.Entity<Category_Product>()
            .HasOne(c => c.Product)
            .WithMany(c => c.Category_Products)
            .HasForeignKey(c => c.ProductId);

        builder.Entity<Image>()
            .HasOne(i => i.Product)
            .WithMany(i => i.Images)
            .HasForeignKey(i => i.ProductId);

        builder.Entity<Image>()
            .HasOne(i => i.Category)
            .WithOne(i => i.Image)
            .HasForeignKey<Image>(i => i.CategoryId);

        builder.Entity<Cart>()
            .HasOne(c => c.User)
            .WithMany(c => c.Carts)
            .HasForeignKey(c => c.UserId);

        builder.Entity<CartDetail>()
            .HasOne(c => c.Cart)
            .WithMany(c => c.CartDetails)
            .HasForeignKey(c => c.CartId);

        builder.Entity<CartDetail>()
            .HasOne(c => c.Product)
            .WithMany(c => c.CartDetails)
            .HasForeignKey(c => c.ProductId);

        builder.Entity<Order>()
            .HasOne(o => o.Address)
            .WithMany(o => o.Orders)
            .HasForeignKey(o => o.AddressId);

        builder.Entity<OrderDetail>()
            .HasOne(c => c.Order)
            .WithMany(c => c.OrderDetails)
            .HasForeignKey(c => c.OrderId);

        builder.Entity<OrderDetail>()
            .HasOne(c => c.Product)
            .WithMany(c => c.OrderDetails)
            .HasForeignKey(c => c.ProductId);

        builder.Entity<Specification>()
            .HasOne(s => s.Product)
            .WithMany(s => s.Specifications)
            .HasForeignKey(s => s.ProductId);
    }
}
