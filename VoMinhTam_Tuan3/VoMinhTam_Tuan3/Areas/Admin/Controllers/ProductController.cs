using VoMinhTam_Tuan3.Models;
using VoMinhTam_Tuan3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace VoMinhTam_Tuan3.Areas.Admin.Controllers
{
    [Area("Admin")] // Controller thuộc khu vực Admin
    [Authorize(Roles = SD.Role_Admin)] // Chỉ cho phép truy cập với vai trò Admin
    public class ProductController : Controller
    {
        readonly IProductRepository productRepository;
        readonly ICategoryRepository categoryRepository;

        public ProductController(
       IProductRepository productRepository,
       ICategoryRepository categoryRepository)
        {
            productRepository = productRepository;
            categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var productList = await productRepository.GetAllAsync();
            return View(productList);
        }
    }
}
