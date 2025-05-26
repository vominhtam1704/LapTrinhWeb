using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VOMINHTAM_KIEMTRA.Models;

namespace VOMINHTAM_KIEMTRA.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly BookDbContext _context;

        public CategoriesController(BookDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                .Include(c => c.Books)
                .ToListAsync();

            return View(categories);
        }
    }
}
