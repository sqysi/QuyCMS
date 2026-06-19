using Microsoft.AspNetCore.Mvc;
using CMS.Data;
using System.Linq;

namespace CMS.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Lấy toàn bộ danh mục sản phẩm để hiển thị lên Menu hoặc bộ lọc Frontend
        [HttpGet]
        public IActionResult GetAll()
        {
            var categoryProducts = _context.CategoriesProducts
                .OrderBy(c => c.Name) // Sắp xếp tên danh mục từ A-Z
                .Select(c => new {
                    c.Id,
                    c.Name,
                    c.Description
                })
                .ToList();

            return Ok(categoryProducts);
        }

        // 2. Lấy chi tiết 1 danh mục sản phẩm theo ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var categoryProduct = _context.CategoriesProducts
                .Where(c => c.Id == id)
                .Select(c => new {
                    c.Id,
                    c.Name,
                    c.Description
                })
                .FirstOrDefault();

            if (categoryProduct == null)
            {
                return NotFound(new { message = "Không tìm thấy danh mục sản phẩm này" });
            }

            return Ok(categoryProduct);
        }
    }
}