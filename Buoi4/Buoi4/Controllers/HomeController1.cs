using Buoi4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace Buoi4.Controllers
{
    public class HomeController1 : Controller
   
    {
        public IActionResult Index()
        {
            var actors = new List<Actor>
    {
            new Actor { Name = "Phuong anh dao", Height = 160, Role= "Mai" },
            new Actor { Name = "Tuan Tran", Height = 170, Role= "Sau" },
            new Actor { Name = "Tran Thanh", Height = 150, Role= "Ba" },
            };
            return View(actors);
        }
    }
}
