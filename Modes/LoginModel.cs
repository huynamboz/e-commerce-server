using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.Modes
{
    public class LoginModel
    {
        [Required]
        [MaxLength(100)]
        public string email { get; set; }
        [Required]
        [MaxLength(250)]
        public string password { get; set; }
    }
}
