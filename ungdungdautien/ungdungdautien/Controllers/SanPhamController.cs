using Microsoft.AspNetCore.Mvc;
using ungdungdautien.Models;

namespace ungdungdautien.Controllers
{
    public class SanPhamController : Controller
    {
        public IActionResult BaiTap2()
        {
            var sanPham = new SanPhamViewModel
            {
                TenSanPham = "Laptop Dell XPS 15",
                GiaBan = 35000000,
                AnhMoTa = "/images/dell-xps15.jpg"  
            };

            return View(sanPham);
        }
    }
}
