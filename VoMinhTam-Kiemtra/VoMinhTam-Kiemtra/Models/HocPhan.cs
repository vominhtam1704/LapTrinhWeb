using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoMinhTam_Kiemtra.Models
{
    [Table("HocPhan")]
    public class HocPhan
    {
        [Key]
        [StringLength(6)]
        public string MaHP { get; set; }

        [Required]
        [StringLength(30)]
        public string TenHP { get; set; }

        public int SoTinChi { get; set; }
        public int? SoLuong { get; set; }
    }
}
