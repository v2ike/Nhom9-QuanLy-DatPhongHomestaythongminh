using homestay.api.Data;
using homestay.api.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace homestay.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Không tìm thấy email"
                });
            }

            if (user.PasswordHash != request.Password)
            {
                return Unauthorized(new
                {
                    message = "Email đúng nhưng mật khẩu sai"
                });
            }

            return Ok(new
            {
                message = "Đăng nhập thành công",
                userId = user.Id,
                fullName = user.FullName,
                email = user.Email,
                role = user.Role
            });
        }
    }
}