using Microsoft.AspNetCore.Mvc;
using CMS.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CMS.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Lấy toàn bộ danh sách sản phẩm
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products
                .OrderByDescending(p => p.Id)
                .Select(p => new {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.ImageUrl,
                    p.StockQuantity,

                    // Giả sử Product có liên kết khóa ngoại tới Category
                })
                .ToList();

            return Ok(products);
        }

        // 2. Lấy chi tiết 1 sản phẩm theo ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _context.Products
                .Where(p => p.Id == id)
                .Select(p => new {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.Description, // Chi tiết sản phẩm thường có mô tả
                    p.ImageUrl,
                    p.StockQuantity,
                })
                .FirstOrDefault();

            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm này trong hệ thống" });
            }

            return Ok(product);
        }

        // 3. Lấy danh sách sản phẩm theo Category (Danh mục)
        //[HttpGet("category/{categoryId}")]
        //public IActionResult GetByCategory(int categoryId)
        //{
        //    var products = _context.Products
        //        .Where(p => p.CategoryId == categoryId)
        //        .Select(p => new {
        //            p.Id,
        //            p.Name,
        //            p.Price,
        //            p.ImageUrl
        //        })
        //        .ToList();

        //    return Ok(products);
        //}
    }
}