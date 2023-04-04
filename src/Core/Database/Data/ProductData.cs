using e_commerce_server.Src.Core.Database.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_server.src.Core.Database.Data
{
    [Table("products")]
    public class ProductData
    {
        [Key]
        [Required]
        public int id { get; set; }
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
        public DateTime created_at { get; set; }
        [Required]
        public DateTime updated_at { get; set;}
        [Required]
        public string thumbnail_url { get; set; }
        [Required]
        public string product_status { get; set; }
        [Required]
        public bool active_status { get; set; }
        public virtual ICollection<ThumbnailData> thumbnails { get; set; }

        public int user_id { get; set; }
        public UserData user { get; set; }
        [Required]
        public int category_id { get; set; }
        public virtual CategoryData category { get; set; }
    }
}
