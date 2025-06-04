using System.ComponentModel.DataAnnotations;

namespace VoMinhTam_Tuan3.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public List<Product>? Products { get; set; }
    }
    public class ProductImage
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;


    }
}
