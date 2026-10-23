using homestay.api.Data;
using homestay.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace homestay.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();

            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            var room = await _context.Rooms
                .FirstOrDefaultAsync(x => x.Id == id);

            if (room == null)
            {
                return NotFound("Không tìm thấy phòng");
            }

            return Ok(room);
        }

        [HttpGet("homestay/{homestayId}")]
        public async Task<IActionResult> GetRoomsByHomestay(int homestayId)
        {
            var rooms = await _context.Rooms
                .Where(x => x.HomestayId == homestayId)
                .ToListAsync();

            return Ok(rooms);
        }
    }
}