using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VOMINHTAM_KIEMTRA.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tên chủ đề không được để trống")]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
