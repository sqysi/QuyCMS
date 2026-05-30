using CMS.Data;
using CMS.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq; // Thêm thư viện này để dùng .FirstOrDefault()

namespace CMS.Backend.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        // "Tiêm" kết nối vào Controller
        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Đường dẫn: /Post hoặc /Post/Index
        // Tham số 'id' được truyền vào từ URL (ví dụ: /Post/Index/5)
        public IActionResult Index()
        {
            var posts = _context.Posts.ToList(); // Lấy tất cả bài viết
            return View(posts);
        }        // ĐÂY LÀ HÀM BẠN ĐANG THIẾU
                 // Đường dẫn: /Post/Details/2
                 // Tham số 'id' được truyền vào từ URL (ví dụ: /Post/Index/5)
        public IActionResult ListByCategory(int? id)
        {
            // 1. Kiểm tra nếu không có id truyền vào thì trả về lỗi hoặc toàn bộ bài viết
            if (id == null)
            {
                return BadRequest("Vui lòng cung cấp mã danh mục.");
            }

            // 2. Sử dụng LINQ với tham số 'id' linh hoạt
            var posts = _context.Posts
                        .Where(p => p.CategoryId == id)
                        .OrderByDescending(p => p.CreatedDate)
                        .Include(p => p.Category)
                        .ToList();


            // 3. Truyền dữ liệu ra View
            return View(posts);
        }

        public IActionResult Details(int id)
        {
            // 1. Truy vấn bài viết theo ID
            // Sử dụng .Include(p => p.Category) để lấy kèm thông tin Danh mục (Join bảng)
            var post = _context.Posts
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            // 2. Kiểm tra nếu không tìm thấy bài viết (tránh lỗi màn hình trắng)
            if (post == null)
            {
                return NotFound(); // Trả về trang lỗi 404
            }

            // 3. Truyền dữ liệu sang View
            return View(post);
        }
    }
}