using Buoi4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Buoi4.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                // Lưu thông tin người dùng vào cơ sở dữ liệu hoặc thực hiện các thao tác khác ở đây.
                return Content("Đăng ký thành công!");
            }
            return View(user);
        }

    }
}
