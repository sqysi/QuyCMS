using CMS.Data;
using CMS.Data.Entities;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            // Lấy dữ liệu THẬT từ bảng Posts trong SQL
            var data = _context.Posts.ToList();
            return View(data);
        }

        // ĐÂY LÀ HÀM BẠN ĐANG THIẾU
        // Đường dẫn: /Post/Details/2
        public IActionResult Details(int id)
        {
            // Tìm bài viết có Id khớp với id trên đường dẫn url
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);

            // Nếu không tìm thấy bài viết nào, trả về trang lỗi 404 chuẩn của hệ thống
            if (post == null)
            {
                return NotFound();
            }

            // Nếu tìm thấy, truyền dữ liệu bài viết sang trang Details.cshtml
            return View(post);
        }
    }
}