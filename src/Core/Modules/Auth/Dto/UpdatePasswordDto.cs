using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace e_commerce_server.src.Core.Modules.Auth.Dto
{
    public class UpdatePasswordDto
    {

        [Required]
        [MaxLength(1000)]
        [PasswordPropertyText]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Mật khẩu phải chứa ít nhất 8 kí tự và có ít nhất 1 ký tự hoa, 1 kí tự thường, 1 chữ số và 1 kí tự đặc biệt.")]
        public string password { get; set; }
        [Required]
        [MaxLength(1000)]
        [PasswordPropertyText]
        public string confirm_password { get; set; }
        [Required]
        public string reset_token { get; set; }
    }
}
