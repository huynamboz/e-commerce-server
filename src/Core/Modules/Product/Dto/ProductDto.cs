using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.Src.Core.Database.Data;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.src.Core.Modules.Product.Dto
{
    public class ProductDto
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
        public string product_status { get; set; }
        [Required]
        public string thumbnail_url { get; set; }
        [Required]
        public string keyword { get; set; }

        [Required]
        public int category_id { get; set; }
    }
}
