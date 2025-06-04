using VoMinhTam_Tuan3.Models;
using VoMinhTam_Tuan3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace VoMinhTam_Tuan3.Controllers
{
//    [Area("Admin")]
//    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ApplicationDbContext _context;
        public ProductController(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        ApplicationDbContext context)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId)
        {
            var products = await _productRepository.GetAllAsync();

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId).ToList();
            }

            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(products);
        }


        public async Task<IActionResult> Add()
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Add(Product product, IFormFile mainImage, IFormFile[] imageFiles)
        {
            if (ModelState.IsValid)
            {
                // Lưu ảnh đại diện
                if (mainImage != null)
                {
                    product.ImageUrl = await SaveImage(mainImage);
                }

                // Lưu sản phẩm trước để lấy ID
                await _productRepository.AddAsync(product);

                // Lưu nhiều ảnh bổ sung
                if (imageFiles != null && imageFiles.Length > 0)
                {
                    foreach (var file in imageFiles)
                    {
                        if (file != null && file.Length > 0)
                        {
                            var url = await SaveImage(file);
                            var image = new ProductImage
                            {
                                ProductId = product.Id,
                                Url = url
                            };
                            _context.ProductImages.Add(image);
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }



        private async Task<string> SaveImage(IFormFile image)
        {
            var fileName = Path.GetFileName(image.FileName);
            var savePath = Path.Combine("wwwroot/images", fileName);
            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            return "/images/" + fileName;
        }




        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();

            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(
    int id,
    Product product,
    IFormFile imageUrl,
    List<IFormFile> newImages,
    List<int> DeletedImageIds
)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existing = await _productRepository.GetByIdAsync(id);

                // Thay ảnh đại diện nếu có
                if (imageUrl != null)
                {
                    existing.ImageUrl = await SaveImage(imageUrl);
                }

                // Xóa ảnh phụ
                if (DeletedImageIds != null && existing.Images != null)
                {
                    existing.Images.RemoveAll(img => DeletedImageIds.Contains(img.Id));
                }

                // Thêm ảnh phụ mới
                if (newImages != null && newImages.Any())
                {
                    foreach (var file in newImages)
                    {
                        var url = await SaveImage(file);
                        existing.Images ??= new List<ProductImage>();
                        existing.Images.Add(new ProductImage { Url = url });
                    }
                }

                // Cập nhật thông tin sản phẩm
                existing.Name = product.Name;
                existing.Price = product.Price;
                existing.Description = product.Description;
                existing.CategoryId = product.CategoryId;

                await _productRepository.UpdateAsync(existing);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
