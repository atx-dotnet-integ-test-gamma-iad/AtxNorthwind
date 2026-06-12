using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Web.Models;

[Table("suppliers", Schema = "northwind_dbo")]
public class Supplier
{
    [Key]
    [Column("supplierid")]
    public int SupplierID { get; set; }

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

    [Column("homepage")]
    public string? HomePage { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
