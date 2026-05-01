namespace Northwind.Web.Models;

public class OrderDetail
{
    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public decimal UnitPrice { get; set; }
    public short Quantity { get; set; }
    public float Discount { get; set; }

    // Computed property (should get [NotMapped])
    public decimal LineTotal => UnitPrice * Quantity * (1 - (decimal)Discount);

    // Navigation properties
    public Order? Order { get; set; }
    public Product? Product { get; set; }
}
