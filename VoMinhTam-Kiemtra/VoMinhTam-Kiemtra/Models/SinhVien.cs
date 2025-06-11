using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoMinhTam_Kiemtra.Models
{
    [Table("SinhVien")]
    public class SinhVien
    {
        [Key]
        [StringLength(10)]
        public string MaSV { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(5)]
        public string GioiTinh { get; set; }

        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [StringLength(100)]
        public string? Hinh { get; set; } // ✅ KHÔNG dùng [Required] vì xử lý riêng bằng HinhUpload

        [Required(ErrorMessage = "Phải chọn ngành học")]
        [StringLength(4)]
        public string MaNganh { get; set; }

        // KHÔNG gắn [Required] ở đây!
        public virtual NganhHoc? NganhHoc { get; set; }
    }
}
