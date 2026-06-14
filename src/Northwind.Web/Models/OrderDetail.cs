using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Web.Models;

[Table("orderdetails", Schema = "northwind_dbo")]
public class OrderDetail
{
    // Composite primary key (OrderID + ProductID) is configured via HasKey() in DbContext
    [Column("orderid")]
    public int OrderID { get; set; }

    [Column("productid")]
    public int ProductID { get; set; }

    [Column("unitprice")]
    public decimal UnitPrice { get; set; }

    [Column("quantity")]
    public short Quantity { get; set; }

    [Column("discount")]
    public float Discount { get; set; }

    // Computed property
    [NotMapped]
    public decimal LineTotal => UnitPrice * Quantity * (1 - (decimal)Discount);

    // Navigation properties
    public Order? Order { get; set; }
    public Product? Product { get; set; }
}
