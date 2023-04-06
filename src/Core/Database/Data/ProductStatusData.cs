using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_server.src.Core.Database.Data
{
    [Table("product_statuses")]
    public class ProductStatusData
    {
        [Key]
        [Required]
        public int id { get; set; }
        public virtual ICollection<ProductData> products { get; set; }
        [Required]
        [StringLength(50)]
        public string status { get; set; }

    }
}
