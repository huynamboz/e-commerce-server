using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.Model
{
    public class userInfoModel
    {
        [Key]
        public int user_id { get; set; }
        [Required]
        [StringLength(20)]
        public string phone_number { get; set; }

        [Required]
        public string email { get; set; }
        public string active_status { get; set; }
        [Required]
        [MaxLength(250)]
        public string name { get; set; }
        public string created_at { get; set; }
        public int roleID { get; set; }
        public string avatar_url { get; set; }
    }
}
