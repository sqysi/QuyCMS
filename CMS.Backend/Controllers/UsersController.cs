using Microsoft.AspNetCore.Mvc;
using CMS.Data;
using System.Linq;

namespace CMS.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Lấy toàn bộ danh sách thành viên quản trị
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _context.Users
                .OrderBy(u => u.Id) // Có thể sắp xếp theo Username nếu muốn
                .Select(u => new {
                    u.Id,
                    u.Username,
                    u.FullName,
                    u.Role
                    // Không lấy PasswordHash để bảo mật an toàn cho API
                })
                .ToList();

            return Ok(users);
        }

        // 2. Lấy chi tiết 1 người dùng theo ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _context.Users
                .Where(u => u.Id == id)
                .Select(u => new {
                    u.Id,
                    u.Username,
                    u.FullName,
                    u.Role
                })
                .FirstOrDefault();

            if (user == null)
            {
                return NotFound(new { message = "Không tìm thấy người dùng này trong hệ thống" });
            }

            return Ok(user);
        }
    }
}