using homestay.api.Data;
using homestay.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace homestay.api.Controllers
{
    // Class DTO nhận dữ liệu đăng nhập
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private const string SuccessMessage = "Đăng nhập thành công";
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email))
            {
                return BadRequest(new { message = "Dữ liệu đăng nhập không hợp lệ" });
            }

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
                message = SuccessMessage,
                userId = user.Id,
                fullName = user.FullName,
                email = user.Email,
                role = user.Role
            });
        }
    }
}