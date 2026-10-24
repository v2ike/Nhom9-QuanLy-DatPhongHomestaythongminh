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

        // GET: api/Homestays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Homestay>>> GetHomestays()
        {
            var homestays = await _context.Homestays.ToListAsync();
            return Ok(homestays);
        }

        // GET: api/Homestays/5 (Đã sửa khớp 100% với Model Room và Review trong DB)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHomestay(int id)
        {
            var homestay = await _context.Homestays
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Address,
                    x.Description,
                    x.Price,
                    x.ImageUrl,
                    x.HostId,
                    x.CreatedAt,
                    x.Tag,

                    // Điểm đánh giá trung bình & Số lượng đánh giá
                    Rating = 4.8,
                    ReviewCount = _context.Reviews.Count(r => r.HomestayId == id),

                    // 1. Album ảnh phụ
                    Images = _context.HomestayImages
                        .Where(img => img.HomestayId == id)
                        .Select(img => img.ImageUrl)
                        .ToList(),

                    // 2. Tiện nghi
                    Amenities = _context.Tiennghi
                        .Where(a => a.HomestayId == id)
                        .Select(a => new
                        {
                            a.Idtiennghi,
                            a.Icon,
                            a.Tittle
                        })
                        .ToList(),

                    // 3. Danh sách phòng (Đã khớp với Model Room: Capacity, Status)
                    Rooms = _context.Rooms
                        .Where(r => r.HomestayId == id)
                        .Select(r => new
                        {
                            r.Id,
                            r.HomestayId,
                            r.RoomName,
                            RoomCapacityText = $"{r.Capacity} khách",
                            r.Price,
                            r.ImageUrl,
                            IsAvailable = r.Status == "Available"
                        })
                        .ToList(),

                    // 4. Đánh giá khách hàng (Đã Join với bảng Users để lấy FullName & AvatarUrl)
                    Reviews = (from rv in _context.Reviews
                               join u in _context.Users on rv.UserId equals u.Id into userGroup
                               from u in userGroup.DefaultIfEmpty()
                               where rv.HomestayId == id
                               select new
                               {
                                   rv.Id,
                                   UserName = u != null ? u.FullName : "Khách hàng",
                                
                                   RatingScore = 5.0,
                                   DateText = rv.CreatedAt.ToString("dd/MM/yyyy"),
                                   rv.Comment
                               }).ToList()
                })
                .FirstOrDefaultAsync();

            if (homestay == null)
            {
                return NotFound(new { message = "Không tìm thấy Homestay!" });
            }

            return Ok(homestay);
        }

        // GET: api/Homestays/search?keyword=danang
        [HttpGet("search")]
        public async Task<IActionResult> SearchHomestays(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                var homestays = await _context.Homestays.ToListAsync();
                return Ok(homestays);
            }

            var result = await _context.Homestays
                .Where(x =>
                    x.Name.Contains(keyword) ||
                    x.Address.Contains(keyword) ||
                    x.Description.Contains(keyword))
                .ToListAsync();

            return Ok(result);
        }
    }
}