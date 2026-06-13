using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Web.Data;
using Northwind.Web.Models;

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
            .SqlQueryRaw<Order>("SELECT * FROM Orders WHERE OrderDate >= CURRENT_TIMESTAMP - 30")
            .ToListAsync();
        return Ok(orders);
    }

    [HttpGet("by-customer/{customerId}")]
    public async Task<IActionResult> GetOrdersByCustomer(string customerId)
    {
        var param = new SqlParameter("@CustomerID", customerId);
        var orders = await _context.Database
            .SqlQueryRaw<Order>(
                "SELECT *, CAST(EXTRACT(epoch FROM CAST(CAST(CURRENT_TIMESTAMP AS TIMESTAMP) AS TIMESTAMP) - CAST(CAST(OrderDate AS TIMESTAMP) AS TIMESTAMP)) / 86400 AS BIGINT) AS DaysAgo FROM Orders WHERE CustomerID = $CustomerID",
                param)
            .ToListAsync();
        return Ok(orders);
    }

    [HttpGet("history/{customerId}")]
    public async Task<IActionResult> GetCustomerOrderHistory(string customerId)
    {
        var param = new SqlParameter("@CustomerID", customerId);
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
                "TO_CHAR(CAST(OrderDate AS TIMESTAMP), 'YYYY') AS OrderYear " +
                "FROM Orders WHERE NOT ShippedDate IS NULL")
            .ToListAsync();
        return Ok(orders);
    }
}

public class OrderHistoryResult
{
    public string ProductName { get; set; } = string.Empty;
    public int Total { get; set; }
}
