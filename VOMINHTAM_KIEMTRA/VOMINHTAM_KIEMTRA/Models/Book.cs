using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VOMINHTAM_KIEMTRA.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Tác giả không được để trống")]
        public string Author { get; set; }

        [Range(1900, 2100, ErrorMessage = "Năm xuất bản không hợp lệ")]
        public int Year { get; set; }

        public string? ImagePath { get; set; }

        [Display(Name = "Chủ đề")]
        public int CategoryId { get; set; }
        public string? Description { get; set; }


        public virtual Category Category { get; set; }
    }
}
