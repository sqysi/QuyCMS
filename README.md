# 🛒 CMS Fashion E-Commerce

Dự án Website Thương Mại Điện Tử chuyên kinh doanh thời trang cao cấp. 
Hệ thống được thiết kế theo kiến trúc Client-Server, áp dụng mô hình MVC cho phần quản trị và API RESTful cho giao diện người dùng.

---

## 🌟 Chức năng nổi bật

| Phân hệ | Tính năng chính |
| :--- | :--- |
| **Dành cho Khách hàng** | Xem danh sách sản phẩm, lọc theo danh mục, đọc bài viết/blog thời trang. |
| **Giỏ hàng & Thanh toán** | Thêm/sửa/xóa sản phẩm trong giỏ, tiến hành đặt hàng, gửi email xác nhận. |
| **Dành cho Quản trị viên** | CMS MVC để thêm/sửa/xóa (CRUD) Sản phẩm, Danh mục, Bài viết, Đơn hàng. |
| **Quản lý Đơn hàng** | Cập nhật trạng thái đơn hàng (Đang giao, Hoàn thành), tự động gửi email thông báo. |

---

## 🚀 Công nghệ & Kiến trúc

* **Backend:** C# ASP.NET Core MVC & Web API.
* **Frontend Client:** React.js, Tailwind CSS, Axios, React Router.
* **Cơ sở dữ liệu:** SQL Server kết hợp Entity Framework Core (Code-First).
* **Công cụ hỗ trợ:** CKEditor 5 (soạn thảo Rich-text), dịch vụ gửi Email SMTP.

---

## ⚙️ Yêu cầu môi trường

Để khởi chạy dự án tại local, máy tính của bạn cần cài đặt:

* **Visual Studio 2022** (hỗ trợ .NET 6/7/8 tùy phiên bản dự án).
* **Node.js** (phiên bản v16.x trở lên).
* **SQL Server** (hoặc SQL Server Express / LocalDB).

---

## 🛠 Hướng dẫn Cài đặt & Khởi chạy

### 1. Thiết lập Backend (C# .NET)

1. Mở file solution (`.sln`) trong thư mục Backend bằng **Visual Studio**.
2. Mở file `appsettings.json` và cập nhật chuỗi kết nối `DefaultConnection` cho phù hợp với máy tính của bạn.
3. Thiết lập thông tin cấu hình Email SMTP trong `appsettings.json` (nếu cần test chức năng gửi mail).
4. Mở **Package Manager Console** và khởi tạo cơ sở dữ liệu:
   ```bash
   Update-Database

  ### 1. Thiết lập Frontend (React)

1. Mở Terminal mới và di chuyển vào thư mục Frontend: **cd CMS.Frontend**.
2. Cài đặt các gói thư viện (chỉ cần chạy lần đầu): "npm install"
3. Khởi động ứng dụng React: "npm start"
4. Truy cập vào http://localhost:3000 trên trình duyệt để sử dụng hệ thống.

📁 Cấu trúc thư mục chính
CMS.Backend/Controllers: Chứa cả Web API (cho React) và MVC Controllers (cho Admin).

CMS.Backend/Views: Chứa giao diện CMS quản trị (Razor Pages).

CMS.Backend/Services: Chứa các service xử lý nghiệp vụ, ví dụ EmailService.

CMS.Frontend/src/components: Các thành phần tái sử dụng (Header, Footer, Cards).

CMS.Frontend/src/api: Các hàm gọi Axios tới Backend.

CMS.Frontend/src/context: Quản lý trạng thái toàn cục (Ví dụ: Giỏ hàng).
