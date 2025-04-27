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
        public class TinTucController : ControllerBase
        {
            private readonly AppDbContext _context;

            public TinTucController(AppDbContext context)
            {
                _context = context;
            }

            // Get all TinTuc
            [HttpGet]
            public async Task<ActionResult<IEnumerable<TinTuc>>> GetAll()
            {
                return await _context.TinTuc.ToListAsync();
            }

            // Get one TinTuc by Id
            [HttpGet("{id}")]
            public async Task<ActionResult<TinTuc>> GetById(int id)
            {
                var tinTuc = await _context.TinTuc.FindAsync(id);
                if (tinTuc == null)
                {
                    return NotFound();
                }

                return tinTuc;
            }

        // Create new TinTuc
        [HttpPost]
        public async Task<ActionResult<TinTuc>> Create(TinTuc tinTuc)
        {
            try
            {
                tinTuc.NgayTao = DateTime.Now;
                _context.TinTuc.Add(tinTuc);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = tinTuc.Id }, tinTuc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Có lỗi xảy ra: {ex.Message}");
            }
        }

        // Update an existing TinTuc
        [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, TinTuc tinTuc)
            {
                if (id != tinTuc.Id)
                {
                    return BadRequest();
                }

                var existingTinTuc = await _context.TinTuc.FindAsync(id);
                if (existingTinTuc == null)
                {
                    return NotFound();
                }

                existingTinTuc.TieuDe = tinTuc.TieuDe;
                existingTinTuc.NoiDung = tinTuc.NoiDung;
                existingTinTuc.TacGia = tinTuc.TacGia;
                existingTinTuc.NgaySua = DateTime.Now;

                await _context.SaveChangesAsync();
                return NoContent();
            }

            // Delete a TinTuc
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var tinTuc = await _context.TinTuc.FindAsync(id);
                if (tinTuc == null)
                {
                    return NotFound();
                }

                _context.TinTuc.Remove(tinTuc);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }
    }

