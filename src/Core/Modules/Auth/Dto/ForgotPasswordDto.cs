using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.src.Core.Modules.Auth.Dto
{
    public class ForgotPasswordDto
    {
        [Required]
        [MaxLength(100)]
        [DefaultValue("user@example.com")]
        public string email { get; set; }
    }
}
