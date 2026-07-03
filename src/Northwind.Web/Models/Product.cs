using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Web.Models;

[Table("products", Schema = "northwind_dbo")]
public class Product
{
    [Key]
    [Column("productid")]
    public int ProductID { get; set; }

    [Column("productname")]
    public string ProductName { get; set; } = string.Empty;

    [Column("supplierid")]
    public int? SupplierID { get; set; }

    [Column("categoryid")]
    public int? CategoryID { get; set; }

    [Column("quantityperunit")]
    public string? QuantityPerUnit { get; set; }

    [Column("unitprice")]
    public decimal? UnitPrice { get; set; }

    [Column("unitsinstock")]
    public short? UnitsInStock { get; set; }

    [Column("unitsonorder")]
    public short? UnitsOnOrder { get; set; }

    [Column("reorderlevel")]
    public short? ReorderLevel { get; set; }

    [Column("discontinued")]
    public bool Discontinued { get; set; }

    // Computed properties (should get [NotMapped])
    [NotMapped]
    public bool IsInStock => UnitsInStock > 0;

    [NotMapped]
    public bool NeedsReorder => UnitsInStock <= ReorderLevel;

    // Navigation properties
    public Category? Category { get; set; }
    public Supplier? Supplier { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
