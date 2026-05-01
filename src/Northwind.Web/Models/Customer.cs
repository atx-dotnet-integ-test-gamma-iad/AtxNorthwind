namespace Northwind.Web.Models;

public class Customer
{
    public string CustomerID { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string? ContactName { get; set; }
    public string? ContactTitle { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }

    // Computed property (should get [NotMapped])
    public string DisplayName => $"{CompanyName} ({ContactName})";

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
