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
        // ----------------------------------------------------------------
        // Table → schema mappings  (target schema: northwind_dbo)
        // ----------------------------------------------------------------
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories", "northwind_dbo");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("suppliers", "northwind_dbo");
        });

        // ----------------------------------------------------------------
        // Customer – table mapping + existing relationship
        // ----------------------------------------------------------------
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customers", "northwind_dbo");

            entity.HasMany(c => c.Orders)
                  .WithOne(o => o.Customer)
                  .HasForeignKey(o => o.CustomerID)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ----------------------------------------------------------------
        // Product – table mapping + bool conversion + existing relationships
        // ----------------------------------------------------------------
        modelBuilder.Entity<Product>(entity =>
        {
            // Products table is mapped via entity-class attributes;
            // relationships and bool conversion are configured here.
            entity.HasOne(p => p.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(p => p.CategoryID)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Supplier)
                  .WithMany(s => s.Products)
                  .HasForeignKey(p => p.SupplierID)
                  .OnDelete(DeleteBehavior.Restrict);

            // Bool → int conversion required by Npgsql for EF Core
            entity.Property(p => p.Discontinued).HasConversion<int>();
        });

        // ----------------------------------------------------------------
        // Order – table mapping
        // ----------------------------------------------------------------
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("orders", "northwind_dbo");
        });

        // ----------------------------------------------------------------
        // OrderDetail – table mapping + composite key + existing relationships
        // ----------------------------------------------------------------
        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("orderdetails", "northwind_dbo");

            entity.HasKey(od => new { od.OrderID, od.ProductID });

            entity.HasOne(od => od.Order)
                  .WithMany(o => o.OrderDetails)
                  .HasForeignKey(od => od.OrderID)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(od => od.Product)
                  .WithMany(p => p.OrderDetails)
                  .HasForeignKey(od => od.ProductID)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(modelBuilder);
    }
}
