using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_server.src.Core.Database.Data
{
    [Table("categories")]
    public class CategoryData
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(50)]
        public string name { get; set; }
        public virtual ICollection<ProductData> products { get; set; }
    }
}
