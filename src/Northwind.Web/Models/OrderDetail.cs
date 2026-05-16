using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Web.Models;

[Table("orderdetails", Schema = "northwind_dbo")]
public class OrderDetail
{
    [Key]
    [Column("orderid", Order = 0)]
    public int OrderID { get; set; }

    [Key]
    [Column("productid", Order = 1)]
    public int ProductID { get; set; }

    [Column("unitprice")]
    public decimal UnitPrice { get; set; }

    [Column("quantity")]
    public short Quantity { get; set; }

    [Column("discount")]
    public float Discount { get; set; }

    // Computed property (should get [NotMapped])
    [NotMapped]
    public decimal LineTotal => UnitPrice * Quantity * (1 - (decimal)Discount);

    // Navigation properties
    public Order? Order { get; set; }
    public Product? Product { get; set; }
}
