using System;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
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
        // -------------------------
        // Category
        // -------------------------
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories", "northwind_dbo");
            entity.Property(e => e.CategoryID).HasColumnName("categoryid");
            entity.Property(e => e.CategoryName).HasColumnName("categoryname");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        // -------------------------
        // Customer
        // -------------------------
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customers", "northwind_dbo");
            entity.Property(e => e.CustomerID).HasColumnName("customerid");
            entity.Property(e => e.CompanyName).HasColumnName("companyname");
            entity.Property(e => e.ContactName).HasColumnName("contactname");
            entity.Property(e => e.ContactTitle).HasColumnName("contacttitle");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Region).HasColumnName("region");
            entity.Property(e => e.PostalCode).HasColumnName("postalcode");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Fax).HasColumnName("fax");

            entity.HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // -------------------------
        // Supplier
        // -------------------------
        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("suppliers", "northwind_dbo");
            entity.Property(e => e.SupplierID).HasColumnName("supplierid");
            entity.Property(e => e.CompanyName).HasColumnName("companyname");
            entity.Property(e => e.ContactName).HasColumnName("contactname");
            entity.Property(e => e.ContactTitle).HasColumnName("contacttitle");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Region).HasColumnName("region");
            entity.Property(e => e.PostalCode).HasColumnName("postalcode");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Fax).HasColumnName("fax");
            entity.Property(e => e.HomePage).HasColumnName("homepage");
        });

        // -------------------------
        // Product
        // -------------------------
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products", "northwind_dbo");
            entity.Property(e => e.ProductID).HasColumnName("productid");
            entity.Property(e => e.ProductName).HasColumnName("productname");
            entity.Property(e => e.SupplierID).HasColumnName("supplierid");
            entity.Property(e => e.CategoryID).HasColumnName("categoryid");
            entity.Property(e => e.QuantityPerUnit).HasColumnName("quantityperunit");
            entity.Property(e => e.UnitPrice).HasColumnName("unitprice");
            entity.Property(e => e.UnitsInStock).HasColumnName("unitsinstock");
            entity.Property(e => e.UnitsOnOrder).HasColumnName("unitsonorder");
            entity.Property(e => e.ReorderLevel).HasColumnName("reorderlevel");
            entity.Property(e => e.Discontinued).HasColumnName("discontinued").HasConversion<int>();

            entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierID)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // -------------------------
        // Order
        // -------------------------
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("orders", "northwind_dbo");
            entity.Property(e => e.OrderID).HasColumnName("orderid");
            entity.Property(e => e.CustomerID).HasColumnName("customerid");
            entity.Property(e => e.EmployeeID).HasColumnName("employeeid");
            entity.Property(e => e.OrderDate).HasColumnName("orderdate");
            entity.Property(e => e.RequiredDate).HasColumnName("requireddate");
            entity.Property(e => e.ShippedDate).HasColumnName("shippeddate");
            entity.Property(e => e.ShipVia).HasColumnName("shipvia");
            entity.Property(e => e.Freight).HasColumnName("freight");
            entity.Property(e => e.ShipName).HasColumnName("shipname");
            entity.Property(e => e.ShipAddress).HasColumnName("shipaddress");
            entity.Property(e => e.ShipCity).HasColumnName("shipcity");
            entity.Property(e => e.ShipRegion).HasColumnName("shipregion");
            entity.Property(e => e.ShipPostalCode).HasColumnName("shippostalcode");
            entity.Property(e => e.ShipCountry).HasColumnName("shipcountry");
        });

        // -------------------------
        // OrderDetail
        // -------------------------
        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("orderdetails", "northwind_dbo");
            entity.HasKey(od => new { od.OrderID, od.ProductID });
            entity.Property(e => e.OrderID).HasColumnName("orderid");
            entity.Property(e => e.ProductID).HasColumnName("productid");
            entity.Property(e => e.UnitPrice).HasColumnName("unitprice");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Discount).HasColumnName("discount");

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
