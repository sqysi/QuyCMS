using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Security.Cryptography; // Bổ sung thư viện mã hóa
using System.Text;                  // Bổ sung thư viện xử lý chuỗi
using CMS.Data; // Thay bằng Namespace của project Data

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        // 1. Mã hóa mật khẩu người dùng nhập vào để đem đi so sánh
        string hashedPassword = HashPassword(password);

        // 2. Kiểm tra tài khoản trong Database (so sánh Username và Mật khẩu ĐÃ MÃ HÓA)
        var user = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);

        if (user != null)
        {
            // 3. Thiết lập danh tính (Claims)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role), // Lưu vai trò: Administrator/Editor
                new Claim("FullName", user.FullName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // 4. Đăng nhập và lưu Cookie vào trình duyệt
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
        return View();
    }

    // Hàm đăng xuất
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }

    // ==========================================
    // HÀM HỖ TRỢ: Mã hóa mật khẩu chuẩn SHA256 (Giống hệt bên UserController)
    // ==========================================
    private string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) return string.Empty;

        using (var sha256 = SHA256.Create())
        {
            // Chuyển chuỗi thành mảng byte
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Chuyển mảng byte ngược lại thành chuỗi Hex string để so sánh
            var builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}