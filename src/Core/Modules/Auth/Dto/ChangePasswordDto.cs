using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace e_commerce_server.src.Core.Modules.Auth.Dto
{
    public class ChangePasswordDto
    {
        [Required]
        [MaxLength(250)]
        [PasswordPropertyText]
        public string password { get; set; }
        [Required]
        [MaxLength(250)]
        [PasswordPropertyText] 
        public string newpassword { get; set; }
        [Required]
        [MaxLength(250)]
        [PasswordPropertyText]
        public string confirmnewpassword { get; set; }
        
    }
}
