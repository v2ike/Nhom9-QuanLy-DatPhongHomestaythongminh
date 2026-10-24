using homestay.api.Data;
using homestay.api.Models;
using Microsoft.AspNetCore.Http;
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

    // Class DTO nhận dữ liệu đăng ký
    public class RegisterRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "customer";
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
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

            // Kiểm tra mật khẩu (PasswordHash hoặc Password tùy thuộc tên thuộc tính trong Model User)
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

        // BỔ SUNG HÀM ĐĂNG KÝ (REGISTER) TẠI ĐÂY
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Vui lòng nhập đầy đủ thông tin đăng ký" });
            }

            // 1. Kiểm tra Email đã tồn tại trong CSDL hay chưa
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (existingUser != null)
            {
                return BadRequest(new { message = "Tài khoản/Email này đã tồn tại!" });
            }

            // 2. Khởi tạo đối tượng User mới
            var newUser = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = request.Password, // Lưu ý: Đổi thành PasswordHash hoặc Password đúng với thuộc tính trong Model User của bạn
                Role = string.IsNullOrEmpty(request.Role) ? "customer" : request.Role,
                CreatedAt = DateTime.Now
            };

            // 3. Lưu xuống SQL Server
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Đăng ký tài khoản thành công!",
                userId = newUser.Id
            });
        }
    }
}