using HuynhNha_2122110299.Data;
using HuynhNha_2122110299.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HuynhNha_2122110299.Request;

namespace HuynhNha_2122110299.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 🛡️ Yêu cầu phải có JWT hợp lệ mới truy cập được
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await _context.Orders
                //.Include(o => o.User)
                //.Include(o => o.OrderDetails)
                .ToListAsync();
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Show(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // POST: api/Order
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] OrderCreateRequest request)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value ?? "1");

            // Tạo đơn hàng
            var order = new Order
            {
                UserId = userId,
              
                Total = request.TotalAmount,
              

                OrderDate = 1,
              
            };

            // Thêm order vào context
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); // Lưu trước để lấy order.Id

            // Tạo các chi tiết đơn hàng
            foreach (var item in request.OrderDetails)
            {
                var detail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Note = item.Note,

                    CreatedBy = userId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    DeletedAt = null
                };

                _context.OrderDetails.Add(detail);
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Tạo đơn hàng thành công",
                Order = order
            });
        }




        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderUpdateRequest request)
        {
            var order = await _context.Orders.FindAsync(id);
            var userId = int.Parse(User.FindFirst("id")?.Value ?? "1");

            if (order == null )
                return NotFound();

            // Cập nhật thông tin đơn hàng nếu có
           
            if (request.TotalAmount.HasValue) order.Total = request.TotalAmount.Value;

           


            await _context.SaveChangesAsync();
            return Ok(order);
        }




        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null )
                return NotFound();

            var userId = int.Parse(User.FindFirst("id")?.Value ?? "1");
            var now = DateTime.Now;


            // ❗ Xoá mềm tất cả OrderDetail liên quan
            foreach (var detail in order.OrderDetails)
            {
                detail.DeletedAt = now;
                detail.DeletedBy = userId;
            }

            await _context.SaveChangesAsync();
            return Ok("Đã xoá mềm đơn hàng và chi tiết liên quan.");
        }


        [HttpPut("restore/{id}")]
        [Authorize]
        public async Task<IActionResult> Restore(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            var userId = int.Parse(User.FindFirst("id")?.Value ?? "1");

            if (order == null)
                return NotFound();

           
            foreach (var detail in order.OrderDetails)
            {
                detail.DeletedAt = null;
                detail.UpdatedAt = DateTime.Now;
                detail.UpdatedBy = userId;
            }

            await _context.SaveChangesAsync();
            return Ok("✅ Đã khôi phục đơn hàng và chi tiết liên quan.");
        }



        [HttpDelete("destroy/{id}")]
        [Authorize]
        public async Task<IActionResult> Destroy(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return NotFound();

            // Xóa OrderDetail trước (nếu có)
            _context.OrderDetails.RemoveRange(order.OrderDetails);

            // Xóa Order
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
            return Ok("🗑️ Đã xoá vĩnh viễn đơn hàng và chi tiết liên quan.");
        }
        // API 1: GetProductByCategoryId
        // GET: api/Product/category/{categoryId}
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductByCategoryId(int categoryId)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                //.Include(p => p.Brand)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();

            if (!products.Any())
            {
                return NotFound("Không tìm thấy sản phẩm trong danh mục này.");
            }

            return Ok(products);
        }


    }
}
