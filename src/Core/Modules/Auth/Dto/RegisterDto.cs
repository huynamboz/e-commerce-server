using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using e_commerce_server.src.Core.Common.Enum;
using e_commerce_server.src.Core.Utils;

namespace e_commerce_server.src.Core.Modules.Auth.Dto
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        [RegularExpression(Regex.EMAIL, ErrorMessage = InterceptorEnum.INVALID_EMAIL)]
        public string email { get; set; } = string.Empty;
        [Required]
        [MaxLength(1000)]
        [PasswordPropertyText]
        [DefaultValue("Password123!")]
        [RegularExpression(Regex.PASSWORD, ErrorMessage = InterceptorEnum.INVALID_PASSWORD)]
        public string password { get; set; } = string.Empty;
        [Required]
        [MaxLength(1000)]
        [PasswordPropertyText]
        [DefaultValue("Password123!")]
        public string confirm_password { get; set; } = string.Empty;
        [Required]
        [MaxLength(250)]
        public string name { get; set; } = string.Empty;
    }
}
