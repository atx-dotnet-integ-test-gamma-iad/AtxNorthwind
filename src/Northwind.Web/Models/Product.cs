namespace Northwind.Web.Models;

public class Product
{
    public int ProductID { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int? SupplierID { get; set; }
    public int? CategoryID { get; set; }
    public string? QuantityPerUnit { get; set; }
    public decimal? UnitPrice { get; set; }
    public short? UnitsInStock { get; set; }
    public short? UnitsOnOrder { get; set; }
    public short? ReorderLevel { get; set; }
    public bool Discontinued { get; set; }

    // Computed properties (should get [NotMapped])
    public bool IsInStock => UnitsInStock > 0;
    public bool NeedsReorder => UnitsInStock <= ReorderLevel;

    // Navigation properties
    public Category? Category { get; set; }
    public Supplier? Supplier { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
