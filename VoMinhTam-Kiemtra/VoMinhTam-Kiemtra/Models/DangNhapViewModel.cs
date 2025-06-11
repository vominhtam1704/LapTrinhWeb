using System;
using System.ComponentModel.DataAnnotations;

namespace VoMinhTam_Kiemtra.Models
{
    public class DangNhapViewModel
    {
        [Required]
        public string MaSV { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }
    }
}
