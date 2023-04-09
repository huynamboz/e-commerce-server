using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.src.Core.Modules.Product.Dto
{
    public class ProductDto
    {
        public int? id { get; set; }
        [Required]
        [StringLength(50)]
        public string name { get; set; }
        [Required]
        [StringLength(5000)]
        public string description { get; set; }
        [Required]
        public int price { get; set; }
        [Required]
        public int discount { get; set; }
        [Required]
        public int status_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        [Required]
        public string? keyword { get; set; }
        public string? address { get; set; }
        public int? user_id { get; set; }
        public List<string> thumbnails { get; set; }
        public string? category_name { get; set; }
        [Required]
        public int category_id { get; set; }
    }
}
