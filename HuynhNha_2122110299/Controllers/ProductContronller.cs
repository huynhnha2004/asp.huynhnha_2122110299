﻿using HuynhNha_2122110299.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using HuynhNha_2122110299.Helper;
using HuynhNha_2122110299.Model;
using HuynhNha_2122110299.Request;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HuynhNha_2122110299.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                //.Include(p => p.Brand)
                .ToListAsync();
            var query = _context.Products.ToQueryString();
            Console.WriteLine(query);

            return Ok(products);
        }

        // GET: api/Product/category/3
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var products = await _context.Products 
                .Include(p => p.Category)
                //.Include(p => p.Brand)
                .Where(p => p.CategoryId == categoryId )
                .ToListAsync();

            return Ok(products);
        }

        //// GET: api/Product/brand/4
        //[HttpGet("brand/{brandId}")]
        //public async Task<IActionResult> GetByBrand(int brandId)
        //{
        //    var products = await _context.Products
        //        .Include(p => p.Category)
        //        .Include(p => p.Brand)
        //        .Where(p => p.BrandId == brandId && p.DeletedAt == null)
        //        .ToListAsync();

        //    return Ok(products);
        //}


        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Show(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                //.Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] ProductStoreRequest request)
        {
            // 1. Lấy userId từ claim
            var userId = int.Parse(User.FindFirst("id")?.Value ?? "0");

            // 2. Xử lý lưu file nếu có
            string? thumbnailPath = null;
            if (request.ThumbnailFile != null && request.ThumbnailFile.Length > 0)
            {
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "product");
                if (!Directory.Exists(uploadsDir))
                    Directory.CreateDirectory(uploadsDir);

                var slug = SlugHelper.ToSlug(request.Name);
                var ext = Path.GetExtension(request.ThumbnailFile.FileName);
                var fileName = $"{slug}{ext}";
                var filePath = Path.Combine(uploadsDir, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await request.ThumbnailFile.CopyToAsync(stream);

                thumbnailPath = fileName;
            }

            // 3. Gán model và lưu DB
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                CategoryId = request.CategoryId,
                ImageUrl = thumbnailPath,
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromForm] ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null )
                return NotFound();

            var userId = int.Parse(User.FindFirst("id")?.Value ?? "0");

            // 1) Xử lý ảnh nếu có upload mới
            if (request.ThumbnailFile != null && request.ThumbnailFile.Length > 0)
            {
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "product");
                if (!Directory.Exists(uploadsDir))
                    Directory.CreateDirectory(uploadsDir);

                // đặt tên file: slug + timestamp
                var slug = SlugHelper.ToSlug(request.Name ?? product.Name);
                var ext = Path.GetExtension(request.ThumbnailFile.FileName);
                var fileName = $"{slug}-{DateTime.Now:yyyyMMddHHmmss}{ext}";
                var filePath = Path.Combine(uploadsDir, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await request.ThumbnailFile.CopyToAsync(stream);

             
            }

            // 2) Cập nhật các field khác nếu bên request có giá trị
            if (!string.IsNullOrWhiteSpace(request.Name))
                product.Name = request.Name;

            if (request.Price.HasValue)
                product.Price = request.Price.Value;

           
            if (!string.IsNullOrWhiteSpace(request.Description))
                product.Description = request.Description;

            if (request.CategoryId.HasValue)
                product.CategoryId = request.CategoryId.Value;

            //if (request.BrandId.HasValue)
            //    product.BrandId = request.BrandId.Value;

         
            await _context.SaveChangesAsync();
            return Ok(product);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            var userId = int.Parse(User.FindFirst("id")?.Value ?? "1");
            if (product == null) return NotFound();

            await _context.SaveChangesAsync();

            return Ok("Đã xóa mềm");
        }


        [HttpPut("restore/{id}")]
        [Authorize]
        public async Task<IActionResult> Restore(int id)
        {
            var product = await _context.Products.FindAsync(id);
            var userId = int.Parse(User.FindFirst("id")?.Value ?? "1");
            if (product == null )
                return NotFound();

            await _context.SaveChangesAsync();
            return Ok("Đã khôi phục");
        }



        // DELETE: api/Product/5
        [HttpDelete("destroy/{id}")]
        public async Task<IActionResult> Destroy(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok("Đã xóa vĩnh viễn");
        }


    }
}
