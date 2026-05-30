using CMS.Data;
using CMS.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CMS.Backend.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        // "Tiêm" kết nối vào Controller
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Lấy dữ liệu từ bảng Users trong SQL
            var data = _context.Users.ToList();
            return View(data);
        }

        // 1. Hàm GET: Dùng để hiển thị giao diện Form cho nhập
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 2. Hàm POST: Dùng để đón dữ liệu từ Form gửi lên và lưu vào SQL
        [HttpPost]
        public IActionResult Create(User model)
        {
            // BƯỚC 1: Mã hóa mật khẩu trước khi lưu
            if (!string.IsNullOrEmpty(model.PasswordHash))
            {
                model.PasswordHash = HashPassword(model.PasswordHash);
            }

            // BƯỚC 2: Thêm dữ liệu vào bộ nhớ tạm của Entity Framework
            _context.Users.Add(model);

            // BƯỚC 3: Ra lệnh ghi dữ liệu thật sự vào SQL Server
            _context.SaveChanges();

            // Tự động quay về trang danh sách
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            // Tìm đối tượng người dùng trong Database bằng Id
            var user = _context.Users.Find(id);

            // Kiểm tra nếu tìm thấy thì mới xóa
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // 1. Hàm GET: Tìm dữ liệu cũ và đổ lên Form
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Tìm người dùng trong Database theo Id
            var user = _context.Users.Find(id);

            if (user == null) return NotFound();

            // Xóa rỗng trường mật khẩu khi hiển thị lên giao diện để bảo mật
            // Người dùng chỉ nhập lại nếu thực sự muốn đổi mật khẩu mới
            user.PasswordHash = "";

            return View(user);
        }

        // 2. Hàm POST: Nhận dữ liệu mới từ người dùng và lưu lại
        [HttpPost]
        public IActionResult Edit(User model)
        {
            // Bước 1: Lấy user hiện tại từ Database lên để so sánh
            var existingUser = _context.Users.Find(model.Id);

            if (existingUser != null)
            {
                // Cập nhật các thông tin cơ bản
                existingUser.Username = model.Username;
                existingUser.FullName = model.FullName;
                existingUser.Role = model.Role;

                // BƯỚC 2: Kiểm tra mật khẩu.
                // CHỈ mã hóa và ghi đè mật khẩu nếu người dùng có nhập dữ liệu vào ô Password
                if (!string.IsNullOrEmpty(model.PasswordHash))
                {
                    existingUser.PasswordHash = HashPassword(model.PasswordHash);
                }

                // Lệnh cập nhật đối tượng vào bộ nhớ tạm
                _context.Users.Update(existingUser);

                // Lưu thay đổi thực sự xuống SQL Server
                _context.SaveChanges();
            }

            // Quay lại trang danh sách để xem kết quả
            return RedirectToAction("Index");
        }

        // ==========================================
        // HÀM HỖ TRỢ: Mã hóa mật khẩu chuẩn SHA256
        // ==========================================
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Chuyển chuỗi thành mảng byte
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Chuyển mảng byte ngược lại thành chuỗi Hex string để lưu vào Database
                var builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}