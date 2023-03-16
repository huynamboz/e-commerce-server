using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.Data
{
    public class product
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int user_id { get; set; }
        [Required]
        [MaxLength(250)]
        public string name { get; set; }
        [Required]
        [MaxLength(2000)]
        public string description { get; set; }
        [Required]
        public int category_id { get; set; }
        public int discount { get; set; }
        public int price { get; set; }
        [Required]
        public string thumbnail_url { get; set; }
        public string created_at { get; set; }
    }
}
