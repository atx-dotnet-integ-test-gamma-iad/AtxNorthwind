using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Web.Models;

[Table("orders", Schema = "northwind_dbo")]
public class Order
{
    [Key]
    [Column("orderid")]
    public int OrderID { get; set; }

    [Column("customerid")]
    public string? CustomerID { get; set; }

    [Column("employeeid")]
    public int? EmployeeID { get; set; }

    [Column("orderdate")]
    public DateTime? OrderDate { get; set; }

    [Column("requireddate")]
    public DateTime? RequiredDate { get; set; }

    [Column("shippeddate")]
    public DateTime? ShippedDate { get; set; }

    [Column("shipvia")]
    public int? ShipVia { get; set; }

    [Column("freight")]
    public decimal? Freight { get; set; }

    [Column("shipname")]
    public string? ShipName { get; set; }

    [Column("shipaddress")]
    public string? ShipAddress { get; set; }

    [Column("shipcity")]
    public string? ShipCity { get; set; }

    [Column("shipregion")]
    public string? ShipRegion { get; set; }

    [Column("shippostalcode")]
    public string? ShipPostalCode { get; set; }

    [Column("shipcountry")]
    public string? ShipCountry { get; set; }

    // Computed properties
    [NotMapped]
    public bool IsShipped => ShippedDate != null;

    [NotMapped]
    public int DaysToShip => ShippedDate.HasValue && OrderDate.HasValue
        ? (ShippedDate.Value - OrderDate.Value).Days : 0;

    // Navigation properties
    public Customer? Customer { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
