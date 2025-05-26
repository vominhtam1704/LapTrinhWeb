using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VOMINHTAM_KIEMTRA.Models;

namespace VOMINHTAM_KIEMTRA.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookDbContext _context;

        public BooksController(BookDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách sách + thống kê chủ đề
        public async Task<IActionResult> Index()
        {
            // Lấy danh sách sách kèm chủ đề
            var books = await _context.Books.Include(b => b.Category).ToListAsync();

            // Lấy thống kê số lượng sách theo từng chủ đề
            var categoryStats = await _context.Categories
                .Select(c => new
                {
                    c.CategoryId,
                    c.Name,
                    Count = c.Books.Count
                }).ToListAsync();

            // Gửi qua ViewBag để hiển thị trong menu trái
            ViewBag.CategoryCounts = categoryStats;

            return View(books);
        }

        // Hiển thị form tạo mới
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
                return View(book);
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/ImageBooks", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                book.ImagePath = fileName;
            }

            _context.Add(book);
            await _context.SaveChangesAsync();

            // ✅ Quay về Index sau khi thêm
            return RedirectToAction(nameof(Index));
        }



        // Hiển thị form chỉnh sửa
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", book.CategoryId);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book, IFormFile imageFile)
        {
            if (id != book.BookId) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", book.CategoryId);
                return View(book);
            }

            // Xử lý ảnh
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/ImageBooks", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                book.ImagePath = fileName;
            }
            else
            {
                // Nếu không upload ảnh mới, giữ ảnh cũ
                var oldBook = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.BookId == id);
                book.ImagePath = oldBook?.ImagePath;
            }

            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(b => b.BookId == book.BookId))
                    return NotFound();
                throw;
            }

            // ✅ Quay về Index sau khi sửa
            return RedirectToAction(nameof(Index));
        }



        // Hiển thị xác nhận xóa
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.Include(b => b.Category).FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }

            // ✅ Quay về trang danh sách sau khi xóa
            return RedirectToAction(nameof(Index));
        }


        // Chi tiết sách
        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null) return NotFound();

            // Thêm ViewBag thống kê như Index
            var categoryStats = await _context.Categories
                .Select(c => new
                {
                    c.CategoryId,
                    c.Name,
                    Count = c.Books.Count
                }).ToListAsync();

            ViewBag.CategoryCounts = categoryStats;

            return View(book);
        }

    }
}
