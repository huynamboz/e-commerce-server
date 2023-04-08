using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.src.Core.Modules.Product.Dto
{
    public class AddProductDto
    {
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
        [DefaultValue(1)]
        public int status_id { get; set; }
        [Required]
        public string? keyword { get; set; }
        [Required]
        [DefaultValue(1)]
        public int category_id { get; set; }
    }
}
