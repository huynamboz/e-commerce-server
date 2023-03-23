using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_server.Data
{
    [Table("thumbnail")]
    public class thumbnail
    {
        [Key]
        public int Id { get; set; }
        public string product_id { get; set; }
        public string FilePath { get; set; }

    }
}
