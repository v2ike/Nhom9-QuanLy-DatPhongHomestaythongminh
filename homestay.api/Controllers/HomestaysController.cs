using homestay.api.Data;
using homestay.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace homestay.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomestaysController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HomestaysController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetHomestays()
        {
            var homestays = await _context.Homestays.ToListAsync();

            return Ok(homestays);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHomestay(int id)
        {
            var homestay = await _context.Homestays
                .FirstOrDefaultAsync(x => x.Id == id);

            if (homestay == null)
            {
                return NotFound();
            }

            return Ok(homestay);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchHomestays(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await GetHomestays();
            }

            var homestays = await _context.Homestays
                .Where(x =>
                    x.Name.Contains(keyword) ||
                    x.Address.Contains(keyword) ||
                    x.Description.Contains(keyword))
                .ToListAsync();

            return Ok(homestays);
        }
    }
}