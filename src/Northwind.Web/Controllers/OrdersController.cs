using System.Data.SqlClient;
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
            .SqlQueryRaw<Order>("SELECT * FROM Orders WHERE OrderDate >= GETDATE() - 30")
            .ToListAsync();
        return Ok(orders);
    }

    [HttpGet("by-customer/{customerId}")]
    public async Task<IActionResult> GetOrdersByCustomer(string customerId)
    {
        var param = new SqlParameter("@CustomerID", customerId);
        var orders = await _context.Database
            .SqlQueryRaw<Order>(
                "SELECT *, DATEDIFF(DAY, OrderDate, GETDATE()) AS DaysAgo FROM Orders WHERE CustomerID = @CustomerID",
                param)
            .ToListAsync();
        return Ok(orders);
    }

    [HttpGet("history/{customerId}")]
    public async Task<IActionResult> GetCustomerOrderHistory(string customerId)
    {
        var param = new SqlParameter("@CustomerID", customerId);
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
                "SELECT *, FORMAT(OrderDate, 'yyyy-MM-dd') AS FormattedDate, " +
                "DATEPART(YEAR, OrderDate) AS OrderYear " +
                "FROM Orders WHERE ShippedDate IS NOT NULL")
            .ToListAsync();
        return Ok(orders);
    }
}

public class OrderHistoryResult
{
    public string ProductName { get; set; } = string.Empty;
    public int Total { get; set; }
}
