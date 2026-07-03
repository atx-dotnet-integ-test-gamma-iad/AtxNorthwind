using Npgsql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Web.Data;
using Northwind.Web.Models;
using System.Threading.Tasks;

namespace Northwind.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly NorthwindDbContext _context;

    public OrdersController(NorthwindDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _context.Database
            .SqlQueryRaw<Order>("SELECT * FROM northwind_dbo.\"Orders\" WHERE OrderDate >= NOW() - INTERVAL '30 days'")
            .ToListAsync();
        return Ok(orders);
    }

    [HttpGet("by-customer/{customerId}")]
    public async Task<IActionResult> GetOrdersByCustomer(string customerId)
    {
        var param = new NpgsqlParameter("@CustomerID", customerId);
        var orders = await _context.Database
            .SqlQueryRaw<Order>(
                "SELECT *, (NOW()::date - OrderDate::date) AS DaysAgo FROM northwind_dbo.\"Orders\" WHERE CustomerID = @CustomerID",
                param)
            .ToListAsync();
        return Ok(orders);
    }

    [HttpGet("history/{customerId}")]
    public async Task<IActionResult> GetCustomerOrderHistory(string customerId)
    {
        var param = new NpgsqlParameter("@CustomerID", customerId);
        // TODO: Manual migration required - Stored procedure call detected.
        // This stored procedure needs to be migrated to PostgreSQL and this call updated accordingly.
        var result = await _context.Database
            .SqlQueryRaw<OrderHistoryResult>(
                "EXEC [dbo].[CustOrderHist] @CustomerID",
                param)
            .ToListAsync();
        return Ok(result);
    }

    [HttpGet("recent")]
    public async Task<IActionResult> GetRecentOrders()
    {
        var orders = await _context.Database
            .SqlQueryRaw<Order>(
                "SELECT *, TO_CHAR(OrderDate, 'YYYY-MM-DD') AS FormattedDate, " +
                "EXTRACT(YEAR FROM OrderDate)::int AS OrderYear " +
                "FROM northwind_dbo.\"Orders\" WHERE ShippedDate IS NOT NULL")
            .ToListAsync();
        return Ok(orders);
    }
}

public class OrderHistoryResult
{
    public string ProductName { get; set; } = string.Empty;
    public int Total { get; set; }
}
