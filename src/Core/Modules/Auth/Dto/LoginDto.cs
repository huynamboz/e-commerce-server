using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.Src.Core.Modules.Auth.Dto
{
    public class LoginModel
    {
        [Required]
        [MaxLength(100)]
        [DefaultValue("string@gmail.com")]
        public string email { get; set; }
        [Required]
        [MaxLength(250)]
        public string password { get; set; }
    }
}
