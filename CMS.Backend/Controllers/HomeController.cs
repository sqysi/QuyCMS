using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMS.Data; // Th? m?c ch?a DbContext 
using System.Linq;
using Microsoft.AspNetCore.Authorization; // Cần thêm namespace này

[Authorize] // Bắt buộc phải đăng nhập mới được vào các hàm bên dưới
public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // LINQ: L?y 3 bài vi?t m?i nh?t
        var latestPosts = _context.Posts
                          .Include(p => p.Category) // L?y kèm tên danh m?c ?? hi?n th? 
                          .OrderByDescending(p => p.CreatedDate) // S?p x?p ngày m?i nh?t lên ??u 
                          .Take(3) // Ch? l?y ?úng 3 b?n tin ??u tiên
                          .ToList();

        return View(latestPosts);
    }
}