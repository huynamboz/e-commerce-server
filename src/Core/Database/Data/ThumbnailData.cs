using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e_commerce_server.src.Core.Database.Data
{
    [Table("thumbnails")]
    public class ThumbnailData
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string thumbnail_url { get; set; }
        [Required]
        public int product_id { get; set; }
        [JsonIgnore]
        public virtual ProductData product { get; set; }
    }
}
