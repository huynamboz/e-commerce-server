using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.Model
{
    public class thumbnailModel
    {
        [Key]
        public int Id { get; set; }
        public string product_id { get; set; }
        public string FilePath { get; set; }
    }
}
