using CMS.Data;
using CMS.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq; // Thêm thư viện này để dùng .FirstOrDefault()
using Microsoft.AspNetCore.Authorization; // Cần thêm namespace này

namespace CMS.Backend.Controllers
{
    [Authorize] // Bắt buộc phải đăng nhập mới được vào các hàm bên dưới

    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostController(
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }        // Đường dẫn: /Post hoặc /Post/Index
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
        // 1. Hàm hiển thị form tạo mới bài viết (GET)
        [HttpGet]
        public IActionResult Create()
        {
            // Chúng ta lấy danh sách Category để đổ vào ViewBag
            ViewBag.CategoryList = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }



        [HttpPost]
        public IActionResult Create(
            Post model,
            IFormFile? uploadImage)
        {
            if (uploadImage != null &&
                uploadImage.Length > 0)
            {
                model.ImageUrl =
                    UploadFile(uploadImage);
            }

            model.CreatedDate = DateTime.Now;

            _context.Posts.Add(model);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var post = _context.Posts.Find(id);

            if (post != null)
            {
                if (!string.IsNullOrEmpty(post.ImageUrl))
                {
                    string path =
                        Path.Combine(
                            _webHostEnvironment.WebRootPath,
                            post.ImageUrl.TrimStart('/')
                        );

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                _context.Posts.Remove(post);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }        // GET: Hiển thị form kèm dữ liệu cũ
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null) return NotFound();

            // Chuẩn bị lại danh sách danh mục để người dùng có thể đổi chuyên mục
            ViewBag.CategoryList = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
            return View(post);
        }

        // POST: Thực hiện cập nhật
        [HttpPost]
        public IActionResult Edit(
            Post model,
            IFormFile? uploadImage)
        {
            var post =
                _context.Posts.Find(model.Id);

            if (post == null)
            {
                return NotFound();
            }

            if (uploadImage != null &&
                uploadImage.Length > 0)
            {
                if (!string.IsNullOrEmpty(post.ImageUrl))
                {
                    string oldPath =
                        Path.Combine(
                            _webHostEnvironment.WebRootPath,
                            post.ImageUrl.TrimStart('/')
                        );

                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                post.ImageUrl =
                    UploadFile(uploadImage);
            }

            post.Title = model.Title;
            post.Content = model.Content;
            post.CategoryId = model.CategoryId;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        private string UploadFile(IFormFile file)
        {
            string uploadsFolder =
                Path.Combine(
                    _webHostEnvironment.WebRootPath,
                    "images"
                );

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileName =
                Guid.NewGuid().ToString()
                + Path.GetExtension(file.FileName);

            string uploadPath =
                Path.Combine(
                    uploadsFolder,
                    fileName
                );

            using (var stream =
                   new FileStream(uploadPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/images/" + fileName;
        }
    }
}