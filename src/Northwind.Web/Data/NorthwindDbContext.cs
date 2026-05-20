using System;
using Microsoft.EntityFrameworkCore;
using Northwind.Web.Models;

namespace Northwind.Web.Data;

public class NorthwindDbContext : DbContext
{
    static NorthwindDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // --- Table mappings (PostgreSQL schema: northwind_dbo) ---
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories", "northwind_dbo");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customers", "northwind_dbo");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("suppliers", "northwind_dbo");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products", "northwind_dbo");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("orders", "northwind_dbo");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("orderdetails", "northwind_dbo");
        });

        // --- Bool conversions (PostgreSQL compatibility) ---
        modelBuilder.Entity<Product>()
            .Property(e => e.Discontinued)
            .HasConversion<int>();

        // --- Relationships ---
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplierID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderDetail>()
            .HasKey(od => new { od.OrderID, od.ProductID });

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Product)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(od => od.ProductID)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}
