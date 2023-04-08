using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.Src.Core.Modules.Auth.Dto
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [MaxLength(1000)]
        [PasswordPropertyText]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Mật khẩu phải chứa ít nhất 8 kí tự và có ít nhất 1 ký tự hoa, 1 kí tự thường, 1 chữ số và 1 kí tự đặc biệt.")]
        public string password { get; set; }
        [Required]
        [MaxLength(250)]
        public string name { get; set; }


    }
}
