using Microsoft.AspNetCore.Mvc;
using CMS.Data; // Thay bằng Namespace của project chứa ApplicationDbContext của bạn
using Microsoft.EntityFrameworkCore; // Thêm để hỗ trợ Include nếu cần

namespace CMS.Backend.Controllers
{
    // 1. Định nghĩa đường dẫn để gọi API. [controller] sẽ tự lấy tên "Posts"
    // Khi chạy, địa chỉ sẽ là: https://localhost:xxxx/api/posts
    [Route("api/[controller]")]

    // 2. Đánh dấu đây là một API Controller để hệ thống hỗ trợ các tính năng RESTful
    [ApiController]

    // 3. API Controller phải kế thừa từ ControllerBase (thay vì Controller như MVC)
    public class PostsController : ControllerBase
    {
        // 4. Khai báo biến kết nối Database
        private readonly ApplicationDbContext _context;

        // 5. Hàm khởi tạo (Constructor): "Tiêm" kết nối Database vào để sử dụng
        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Chỉ định đây là phương thức GET (Dùng để lấy dữ liệu toàn bộ)
        [HttpGet]
        public IActionResult GetAll()
        {
            // Lấy dữ liệu từ bảng Posts
            var posts = _context.Posts
                .OrderByDescending(p => p.Id) // Sắp xếp bài mới nhất lên đầu
                .Select(p => new {            // "Gọt tỉa" dữ liệu: chỉ lấy những trường cần thiết (không có content)
                    p.Id,
                    p.Title,
                    p.ImageUrl,
                    p.CreatedDate,
                    CategoryName = p.Category.Name // Lấy tên danh mục thay vì chỉ lấy ID
                })
                .ToList();

            // Trả về kết quả cho Frontend kèm mã trạng thái 200 (Thành công)
            return Ok(posts);
        }

        // ===========================================================================
        // BỔ SUNG THEO YÊU CẦU: BƯỚC 2 & BƯỚC 3 (Lấy chi tiết bài viết và xử lý lỗi 404)
        // Đường dẫn gọi: GET https://localhost:xxxx/api/posts/{id}
        // ===========================================================================
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Tìm bài viết theo ID từ database
            var post = _context.Posts
                .Where(p => p.Id == id)
                .Select(p => new {
                    p.Id,
                    p.Title,
                    p.ImageUrl,
                    p.CreatedDate,
                    p.Content, // THÊM trường content ở màn hình chi tiết theo BƯỚC 2
                    CategoryName = p.Category.Name
                })
                .FirstOrDefault();

            // BƯỚC 3: Nếu bài viết không tồn tại (post == null)
            if (post == null)
            {
                // Trả về mã trạng thái 404 Not Found kèm chuỗi JSON thông báo lỗi
                return NotFound(new { message = "Không tìm thấy bài viết này trong hệ thống" });
            }

            // BƯỚC 2: Trả về duy nhất 1 đối tượng bài viết (mã trạng thái 200 OK)
            return Ok(post);
        }

        // 2. Định nghĩa đường dẫn có tham số: api/posts/category/{id}
        [HttpGet("category/{categoryId}")]
        public IActionResult GetByCategory(int categoryId)
        {
            // Lọc các bài viết có CategoryId trùng với ID truyền vào từ URL
            var posts = _context.Posts
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new {
                    p.Id,
                    p.Title,
                    p.ImageUrl,
                    p.CreatedDate
                })
                .ToList();

            return Ok(posts);
        }
    }
}