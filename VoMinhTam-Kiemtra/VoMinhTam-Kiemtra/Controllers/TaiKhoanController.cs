using Microsoft.AspNetCore.Mvc;
using VoMinhTam_Kiemtra.Models;
using Microsoft.EntityFrameworkCore;

namespace VoMinhTam_Kiemtra.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly Test1Context _context;

        public TaiKhoanController(Test1Context context)
        {
            _context = context;
        }

        // GET: /TaiKhoan/DangNhap
        public IActionResult DangNhap()
        {
            return View();
        }

        // POST: /TaiKhoan/DangNhap
        [HttpPost]
        public async Task<IActionResult> DangNhap(DangNhapViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sv = await _context.SinhViens
                    .FirstOrDefaultAsync(s => s.MaSV == model.MaSV && s.NgaySinh == model.NgaySinh);

                if (sv != null)
                {
                    HttpContext.Session.SetString("MaSV", sv.MaSV);
                    HttpContext.Session.SetString("HoTen", sv.HoTen);
                    return RedirectToAction("Index", "SinhVien");
                }

                ViewBag.ThongBao = "Sai Mã SV hoặc Ngày Sinh!";
            }
            return View(model);
        }

        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("DangNhap");
        }
    }
}
