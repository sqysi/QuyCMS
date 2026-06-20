using CMS.Data;
using CMS.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CMS.Backend.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // ==========================
        // DANH SÁCH SẢN PHẨM
        // ==========================
        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.CategoryProduct)
                .OrderByDescending(p => p.Id)
                .ToList();

            return View(products);
        }

        // ==========================
        // CREATE - GET
        // ==========================
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(
                _context.CategoriesProducts.ToList(),
                "Id",
                "Name"
            );

            return View();
        }

        // ==========================
        // CREATE - POST
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            Product model,
            IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = new SelectList(
                    _context.CategoriesProducts.ToList(),
                    "Id",
                    "Name"
                );

                return View(model);
            }

            if (imageFile != null &&
                imageFile.Length > 0)
            {
                model.ImageUrl = UploadFile(imageFile);
            }

            _context.Products.Add(model);
            _context.SaveChanges();

            TempData["Success"] =
                "Thêm sản phẩm thành công";

            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // EDIT - GET
        // ==========================
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product =
                _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.CategoryList =
                new SelectList(
                    _context.CategoriesProducts.ToList(),
                    "Id",
                    "Name",
                    product.CategoryProductId
                );

            return View(product);
        }

        // ==========================
        // EDIT - POST
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            Product model,
            IFormFile? imageFile)
        {
            var product =
                _context.Products.Find(model.Id);

            if (product == null)
            {
                return NotFound();
            }

            if (imageFile != null &&
                imageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    string oldPath =
                        Path.Combine(
                            _webHostEnvironment.WebRootPath,
                            product.ImageUrl.TrimStart('/')
                        );

                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                product.ImageUrl =
                    UploadFile(imageFile);
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.StockQuantity = model.StockQuantity;
            product.CategoryProductId = model.CategoryProductId;

            _context.SaveChanges();

            TempData["Success"] =
                "Cập nhật sản phẩm thành công";

            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // DELETE
        // ==========================
        public IActionResult Delete(int id)
        {
            var product =
                _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                string path =
                    Path.Combine(
                        _webHostEnvironment.WebRootPath,
                        product.ImageUrl.TrimStart('/')
                    );

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            TempData["Success"] =
                "Xóa sản phẩm thành công";

            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // UPLOAD FILE
        // ==========================
        private string UploadFile(IFormFile file)
        {
            string uploadsFolder =
                Path.Combine(
                    _webHostEnvironment.WebRootPath,
                    "images"
                );

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(
                    uploadsFolder
                );
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