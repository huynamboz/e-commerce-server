using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using e_commerce_server.src.Core.Utils;
using e_commerce_server.src.Core.Common.Enum;

namespace e_commerce_server.src.Core.Modules.Auth.Dto
{
    public class ChangePasswordDto
    {
        [Required]
        [MaxLength(1000)]
        [PasswordPropertyText]
        public string current_password { get; set; } = string.Empty;
        [Required]
        [MaxLength(1000)]
        [PasswordPropertyText] 
        [RegularExpression(Regex.PASSWORD, ErrorMessage = InterceptorEnum.INVALID_PASSWORD)]
        public string new_password { get; set; } = string.Empty;
        [Required]
        [MaxLength(1000)]
        [PasswordPropertyText]
        public string confirm_password { get; set; } = string.Empty;
    }
}
