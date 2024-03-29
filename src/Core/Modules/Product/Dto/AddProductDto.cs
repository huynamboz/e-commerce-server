using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using e_commerce_server.src.Core.Common.Enum;

namespace e_commerce_server.src.Core.Modules.Product.Dto
{
    public class AddProductDto
    {
        [Required]
        [StringLength(50)]
        public string name { get; set; } = string.Empty;
        [Required]
        [StringLength(5000)]
        public string description { get; set; } = string.Empty;
        [Required]
        public int price { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = InterceptorEnum.INVALID_DISCOUNT_NUMBER)]
        public int discount { get; set; }   
        [Required]
        [DefaultValue(1)]
        public int status_id { get; set; }
        [Required]
        [DefaultValue(1)]
        public int category_id { get; set; }
        [Required]
        public List<string> thumbnailUrls { get; set; } = new List<string>();
    }
}
