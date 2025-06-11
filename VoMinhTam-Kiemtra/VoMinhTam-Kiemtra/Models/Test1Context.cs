using Microsoft.EntityFrameworkCore;

namespace VoMinhTam_Kiemtra.Models
{
    public class Test1Context : DbContext
    {
        public Test1Context(DbContextOptions<Test1Context> options) : base(options)
        {
        }

        // Khai báo các DbSet (tương ứng với bảng trong CSDL)
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<NganhHoc> NganhHocs { get; set; }
        public DbSet<HocPhan> HocPhans { get; set; }
        public DbSet<DangKy> DangKys { get; set; }
        public DbSet<ChiTietDangKy> ChiTietDangKys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Quan hệ: SinhVien ↔ NganhHoc
            modelBuilder.Entity<SinhVien>()
                .HasOne(sv => sv.NganhHoc)
                .WithMany(ng => ng.SinhViens)
                .HasForeignKey(sv => sv.MaNganh);

            // ✅ Cấu hình khóa chính tổ hợp cho ChiTietDangKy
            modelBuilder.Entity<ChiTietDangKy>()
                .HasKey(c => new { c.MaDK, c.MaHP });

            // Quan hệ: ChiTietDangKy ↔ DangKy
            modelBuilder.Entity<ChiTietDangKy>()
                .HasOne(c => c.DangKy)
                .WithMany(d => d.ChiTietDangKys)
                .HasForeignKey(c => c.MaDK);

            // Quan hệ: ChiTietDangKy ↔ HocPhan
            modelBuilder.Entity<ChiTietDangKy>()
                .HasOne(c => c.HocPhan)
                .WithMany()
                .HasForeignKey(c => c.MaHP);
        }
    }
}
