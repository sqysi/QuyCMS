using Microsoft.AspNetCore.Mvc;
using CMS.Data;
using System.Linq;

namespace CMS.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Lấy danh sách toàn bộ khách hàng
        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _context.Customers
                .OrderByDescending(c => c.Id) // Khách hàng mới nhất lên đầu
                .Select(c => new {
                    c.Id,
                    c.FullName,
                    c.Email,
                    c.Phone,
                    c.Address
                    // Bỏ qua trường Password
                })
                .ToList();

            return Ok(customers);
        }

        // 2. Lấy chi tiết thông tin 1 khách hàng theo ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var customer = _context.Customers
                .Where(c => c.Id == id)
                .Select(c => new {
                    c.Id,
                    c.FullName,
                    c.Email,
                    c.Phone,
                    c.Address
                })
                .FirstOrDefault();

            if (customer == null)
            {
                return NotFound(new { message = "Không tìm thấy thông tin khách hàng này" });
            }

            return Ok(customer);
        }
    }
}