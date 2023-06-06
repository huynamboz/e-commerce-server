using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using e_commerce_server.src.Core.Utils;
using e_commerce_server.src.Core.Common.Enum;

namespace e_commerce_server.src.Core.Modules.Auth.Dto
{
    public class ResetPasswordDto
    {

        [Required]
        [MaxLength(1000)]
        [PasswordPropertyText]
        [RegularExpression(Regex.PASSWORD, ErrorMessage = InterceptorEnum.INVALID_PASSWORD)]
        public string password { get; set; } = string.Empty;
        [Required]
        [MaxLength(1000)]
        [PasswordPropertyText]
        public string confirm_password { get; set; } = string.Empty;
        [Required]
        public string reset_token { get; set; } = string.Empty;
    }
}
