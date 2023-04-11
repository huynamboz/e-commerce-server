using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e_commerce_server.src.Core.Database.Data
{
    [Table("product_statuses")]
    public class ProductStatusData
    {
        [Key]
        [Required]
        public int id { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductData> products { get; set; }
        [Required]
        [StringLength(50)]
        public string status { get; set; }
        public ProductStatusData()
        {
            this.products = new HashSet<ProductData>();
        }
    }
}
