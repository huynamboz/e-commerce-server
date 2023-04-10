using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.src.Core.Modules.Auth.Dto
{
    public class LoginDto
    {
        [Required]
        [MaxLength(100)]
        [DefaultValue("user@example.com")]
        public string email { get; set; }
        [Required]
        [MaxLength(250)]
        [PasswordPropertyText]
        [DefaultValue("Password123!")]
        public string password { get; set; }
    }
}
