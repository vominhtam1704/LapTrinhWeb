using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoMinhTam_Kiemtra.Models
{
    [Table("ChiTietDangKy")]
    public class ChiTietDangKy
    {
        // Không đánh [Key] riêng lẻ ở đây vì dùng khóa chính tổ hợp
        public int MaDK { get; set; }

        [Required]
        [StringLength(6)]
        public string MaHP { get; set; }

        // ✅ Gán rõ ForeignKey để EF không tự tạo tên sai như DangKyMaDK, HocPhanMaHP
        [ForeignKey("MaDK")]
        public virtual DangKy DangKy { get; set; }

        [ForeignKey("MaHP")]
        public virtual HocPhan HocPhan { get; set; }
    }
}
