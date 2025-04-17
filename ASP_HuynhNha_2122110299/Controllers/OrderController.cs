using Microsoft.AspNetCore.Mvc;
using ASP_HuynhNha_2122110299.Data;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Order/user/{userId}
    [HttpGet("user/{userId}")]
    public IActionResult GetOrdersByUserId(int userId)
    {
        var orders = _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderDetails)
            .ToList();

        return Ok(orders);
    }

    // GET: api/Order/detail/{orderId}
    [HttpGet("detail/{orderId}")]
    public IActionResult GetOrderDetail(int orderId)
    {
        var order = _context.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .FirstOrDefault(o => o.OrderId == orderId);

        if (order == null)
            return NotFound();

        return Ok(order);
    }
}
