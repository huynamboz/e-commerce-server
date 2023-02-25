using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_server.Data
{
    [Table("user")]
    public class user
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(100)]
        public string user_name { get; set; }
        [Required]
        [MaxLength(250)]
        public string password { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }
}
