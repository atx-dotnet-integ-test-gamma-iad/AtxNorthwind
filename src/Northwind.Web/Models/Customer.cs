using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Web.Models;

[Table("customers", Schema = "northwind_dbo")]
public class Customer
{
    [Key]
    [Column("customerid")]
    public string CustomerID { get; set; } = string.Empty;

    [Column("companyname")]
    public string CompanyName { get; set; } = string.Empty;

    [Column("contactname")]
    public string? ContactName { get; set; }

    [Column("contacttitle")]
    public string? ContactTitle { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("city")]
    public string? City { get; set; }

    [Column("region")]
    public string? Region { get; set; }

    [Column("postalcode")]
    public string? PostalCode { get; set; }

    [Column("country")]
    public string? Country { get; set; }

    [Column("phone")]
    public string? Phone { get; set; }

    [Column("fax")]
    public string? Fax { get; set; }

    // Computed property (should get [NotMapped])
    [NotMapped]
    public string DisplayName => $"{CompanyName} ({ContactName})";

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
