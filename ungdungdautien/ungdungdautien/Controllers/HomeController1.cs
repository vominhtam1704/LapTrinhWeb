using Microsoft.AspNetCore.Mvc;

namespace ungdungdautien.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Hello, World!";
            ViewData["key"] = "Hello, World!";
            return View();
        }
    }
}
