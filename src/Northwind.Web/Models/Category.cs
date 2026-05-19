using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Web.Models;

[Table("categories", Schema = "public")]
public class Category
{
    [Key]
    [Column("categoryid")]
    public int CategoryID { get; set; }

    [Column("categoryname")]
    public string CategoryName { get; set; } = string.Empty;

    [Column("description")]
    public string? Description { get; set; }

    [Column("picture")]
    public byte[]? Picture { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
