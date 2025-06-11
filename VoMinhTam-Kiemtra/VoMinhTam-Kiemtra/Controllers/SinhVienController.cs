using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoMinhTam_Kiemtra.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace VoMinhTam_Kiemtra.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly Test1Context _context;

        public SinhVienController(Test1Context context)
        {
            _context = context;
        }

        // GET: /SinhVien
        public async Task<IActionResult> Index()
        {
            var sinhViens = await _context.SinhViens.Include(s => s.NganhHoc).ToListAsync();
            return View(sinhViens);
        }

        // GET: /SinhVien/Create
        public IActionResult Create()
        {
            ViewBag.NganhList = _context.NganhHocs.ToList();
            return View();
        }

        // POST: /SinhVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SinhVien sv, IFormFile HinhUpload)
        {
            if (!ModelState.IsValid)
            {
                // Ghi lỗi ra để kiểm tra
                ViewBag.Loi = string.Join(" | ", ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage));
                ViewBag.NganhList = _context.NganhHocs.ToList();
                return View(sv);
            }

            // Xử lý ảnh upload
            if (HinhUpload != null && HinhUpload.Length > 0)
            {
                string folder = "Content/images";
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileName = Path.GetFileName(HinhUpload.FileName);
                string fullPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await HinhUpload.CopyToAsync(stream);
                }

                sv.Hinh = "/" + folder + "/" + fileName;
            }

            _context.Add(sv);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /SinhVien/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            var sv = await _context.SinhViens.FindAsync(id);
            if (sv == null) return NotFound();

            ViewBag.NganhList = _context.NganhHocs.ToList();
            return View(sv);
        }

        // POST: /SinhVien/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SinhVien sv, IFormFile HinhUpload)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.NganhList = _context.NganhHocs.ToList();
                return View(sv);
            }

            // Xử lý ảnh nếu người dùng chọn ảnh mới
            if (HinhUpload != null && HinhUpload.Length > 0)
            {
                string folder = "Content/images";
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileName = Path.GetFileName(HinhUpload.FileName);
                string fullPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await HinhUpload.CopyToAsync(stream);
                }

                sv.Hinh = "/" + folder + "/" + fileName;
            }

            _context.Update(sv);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /SinhVien/Details/{id}
        public async Task<IActionResult> Details(string id)
        {
            var sv = await _context.SinhViens.Include(s => s.NganhHoc)
                                .FirstOrDefaultAsync(s => s.MaSV == id);
            if (sv == null) return NotFound();

            return View(sv);
        }

        // GET: /SinhVien/Delete/{id}
        public async Task<IActionResult> Delete(string id)
        {
            var sv = await _context.SinhViens.Include(s => s.NganhHoc)
                                .FirstOrDefaultAsync(s => s.MaSV == id);
            if (sv == null) return NotFound();

            return View(sv);
        }

        // POST: /SinhVien/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sv = await _context.SinhViens.FindAsync(id);
            if (sv != null)
            {
                _context.SinhViens.Remove(sv);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
