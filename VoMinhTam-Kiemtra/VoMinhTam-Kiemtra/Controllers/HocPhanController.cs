using Microsoft.AspNetCore.Mvc;
using VoMinhTam_Kiemtra.Models;
using VoMinhTam_Kiemtra.Helpers;
using Microsoft.EntityFrameworkCore;

namespace VoMinhTam_Kiemtra.Controllers
{
    public class HocPhanController : Controller
    {
        private readonly Test1Context _context;

        public HocPhanController(Test1Context context)
        {
            _context = context;
        }

        // Câu 3: Hiển thị danh sách học phần
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("MaSV") == null)
                return RedirectToAction("DangNhap", "TaiKhoan");

            var hocPhans = _context.HocPhans.ToList();
            return View(hocPhans);
        }

        // Thêm học phần vào giỏ
        public IActionResult ThemVaoGio(string id)
        {
            var ds = HttpContext.Session.GetObject<List<string>>("GioHP") ?? new List<string>();
            if (!ds.Contains(id))
                ds.Add(id);

            HttpContext.Session.SetObject("GioHP", ds);
            return RedirectToAction("GioDangKy");
        }

        // Xoá từng học phần khỏi giỏ
        public IActionResult XoaKhoiGio(string id)
        {
            var ds = HttpContext.Session.GetObject<List<string>>("GioHP") ?? new List<string>();
            ds.Remove(id);
            HttpContext.Session.SetObject("GioHP", ds);
            return RedirectToAction("GioDangKy");
        }

        // Xoá toàn bộ giỏ
        public IActionResult XoaToanBo()
        {
            HttpContext.Session.Remove("GioHP");
            return RedirectToAction("GioDangKy");
        }

        // Câu 4: Xem giỏ học phần
        public IActionResult GioDangKy()
        {
            if (HttpContext.Session.GetString("MaSV") == null)
                return RedirectToAction("DangNhap", "TaiKhoan");

            var ds = HttpContext.Session.GetObject<List<string>>("GioHP") ?? new List<string>();
            var hocPhans = _context.HocPhans.Where(h => ds.Contains(h.MaHP)).ToList();
            return View(hocPhans);
        }

        // Câu 5: Lưu học phần vào CSDL
        [HttpPost]
        public async Task<IActionResult> LuuDangKy()
        {
            var maSV = HttpContext.Session.GetString("MaSV");
            var ds = HttpContext.Session.GetObject<List<string>>("GioHP") ?? new List<string>();

            if (string.IsNullOrEmpty(maSV) || ds.Count == 0)
            {
                TempData["ThongBao"] = "❗ Bạn chưa chọn học phần nào để lưu!";
                return RedirectToAction("GioDangKy");
            }

            // 1. Tạo đăng ký mới
            var dk = new DangKy
            {
                MaSV = maSV,
                NgayDK = DateTime.Now
            };
            _context.DangKys.Add(dk);
            await _context.SaveChangesAsync(); // lưu để lấy MaDK

            // 2. Thêm học phần vào ChiTietDangKy
            foreach (var maHP in ds)
            {
                // 1. Thêm chi tiết đăng ký
                var ct = new ChiTietDangKy
                {
                    MaDK = dk.MaDK,
                    MaHP = maHP
                };
                _context.ChiTietDangKys.Add(ct);

                // 2. Trừ số lượng học phần
                var hp = await _context.HocPhans.FindAsync(maHP);
                if (hp != null && hp.SoLuong > 0)
                {
                    hp.SoLuong -= 1;
                    _context.HocPhans.Update(hp);
                }
            }


            await _context.SaveChangesAsync();

            // 3. Xoá giỏ sau khi lưu
            HttpContext.Session.Remove("GioHP");

            TempData["ThongBao"] = "✅ Đăng ký thành công!";
            return RedirectToAction("DanhSachDangKy");
        }

        // Hiển thị danh sách học phần đã đăng ký
        public IActionResult DanhSachDangKy()
        {
            var maSV = HttpContext.Session.GetString("MaSV");

            var dangKys = _context.DangKys
                .Where(d => d.MaSV == maSV)
                .OrderByDescending(d => d.NgayDK)
                .Select(d => new
                {
                    d.MaDK,
                    d.NgayDK,
                    ChiTiet = _context.ChiTietDangKys
                                .Where(c => c.MaDK == d.MaDK)
                                .Join(_context.HocPhans, c => c.MaHP, h => h.MaHP, (c, h) => h)
                                .ToList()
                }).ToList();

            ViewBag.DanhSach = dangKys;
            return View();
        }
    }
}
