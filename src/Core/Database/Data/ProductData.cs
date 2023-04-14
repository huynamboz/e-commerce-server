using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_server.src.Core.Database.Data
{
    [Table("products")]
    public class ProductData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [StringLength(50)]
        public string name { get; set; }
        [StringLength(5000)]
        public string description { get; set; }
        public int price { get; set; }
        public int discount { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set;}
        public DateTime delete_at { get; set; }
        public int status_id { get; set; }
        public virtual ProductStatusData product_status { get; set; }
        public virtual ICollection<ThumbnailData> thumbnails { get; set; }
        public int user_id { get; set; }
        public virtual UserData user { get; set; }
        public int category_id { get; set; }
        public virtual CategoryData category { get; set; }
        public virtual ICollection<FavoriteData> favorites { get; set; }

        public ProductData()
        {
            this.thumbnails = new List<ThumbnailData>();
            this.favorites = new List<FavoriteData>();
        }
    }
}
