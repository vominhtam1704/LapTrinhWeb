using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoMinhTam_Kiemtra.Models
{
    [Table("DangKy")]
    public class DangKy
    {
        [Key]
        public int MaDK { get; set; }

        public DateTime NgayDK { get; set; }

        [Required]
        [StringLength(10)]
        public string MaSV { get; set; }

        // ✅ Bổ sung rõ ForeignKey ở đây để EF không tự tạo cột "SinhVienMaSV"
        [ForeignKey("MaSV")]
        public virtual SinhVien SinhVien { get; set; }

        public virtual ICollection<ChiTietDangKy> ChiTietDangKys { get; set; }
    }
}
