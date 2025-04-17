using ASP_HuynhNha_2122110299.Data;
using ASP_HuynhNha_2122110299.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class OrderDetailController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrderDetailController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
    {
        return await _context.OrderDetails.Include(od => od.Product).Include(od => od.Order).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
    {
        var orderDetail = await _context.OrderDetails
            .Include(od => od.Product)
            .Include(od => od.Order)
            .FirstOrDefaultAsync(od => od.OrderDetailId == id);

        if (orderDetail == null)
            return NotFound();

        return orderDetail;
    }

    [HttpPost]
    public async Task<ActionResult<OrderDetail>> CreateOrderDetail(OrderDetail orderDetail)
    {
        _context.OrderDetails.Add(orderDetail);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOrderDetail), new { id = orderDetail.OrderDetailId }, orderDetail);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetail orderDetail)
    {
        if (id != orderDetail.OrderDetailId)
            return BadRequest();

        _context.Entry(orderDetail).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderDetail(int id)
    {
        var orderDetail = await _context.OrderDetails.FindAsync(id);
        if (orderDetail == null)
            return NotFound();

        _context.OrderDetails.Remove(orderDetail);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
