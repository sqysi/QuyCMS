using CMS.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace CMS.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // ========================================================
            // THÊM: Cấu hình các dịch vụ Swagger cho Web API
            // ========================================================
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // ========================================================

            // Đăng ký DbContext vào hệ thống (GIỮ NGUYÊN)
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Khai báo dịch vụ xác thực Cookie (GIỮ NGUYÊN)
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                });
            // 1. Khai báo chính sách CORS
            builder.Services.AddCors(options => {
                options.AddPolicy("AllowAll", policy => {
                    // Cho phép mọi nguồn (Origin), mọi phương thức (GET, POST...), mọi tiêu đề (Header)
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            // Nếu là môi trường Development, vẫn kích hoạt giao diện lỗi đầy đủ của lập trình viên
            else
            {
                // ========================================================
                // THÊM: Kích hoạt giao diện Swagger khi chạy thử nghiệm
                // ========================================================
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    // Cấu hình hiển thị Swagger làm trang mặc định khi chạy dự án (tùy chọn)
                    // Nếu bỏ chú thích dòng dưới, khi bấm F5 sẽ vào thẳng giao diện Swagger luôn
                    // c.RoutePrefix = string.Empty; 
                });
                // ========================================================
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            // Định tuyến cấu hình MVC View cũ (GIỮ NGUYÊN)
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}