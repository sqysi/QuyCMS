using Microsoft.AspNetCore.Mvc;
using CMS.Data;
using System.Linq;

namespace CMS.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Lấy toàn bộ danh mục (Dùng để hiển thị Menu hoặc Filter)
        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _context.Categories
                .OrderBy(c => c.Name) // Sắp xếp theo tên A-Z
                .Select(c => new {
                    c.Id,
                    c.Name,
                    c.Description
                })
                .ToList();

            return Ok(categories);
        }

        // 2. Lấy chi tiết 1 danh mục theo ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new {
                    c.Id,
                    c.Name,
                    c.Description
                })
                .FirstOrDefault();

            if (category == null)
            {
                return NotFound(new { message = "Không tìm thấy danh mục này" });
            }

            return Ok(category);
        }
    }
}