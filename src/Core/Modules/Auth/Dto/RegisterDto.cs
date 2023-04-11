using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.src.Core.Modules.Auth.Dto
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$",
        ErrorMessage = "Định dạng email không hợp lệ")]
        public string email { get; set; }
        [Required]
        [MaxLength(1000)]
        [PasswordPropertyText]
        [DefaultValue("Password123!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Mật khẩu phải chứa ít nhất 8 kí tự và có ít nhất 1 ký tự hoa, 1 kí tự thường, 1 chữ số và 1 kí tự đặc biệt.")]
        public string password { get; set; }
        [Required]
        [MaxLength(1000)]
        [PasswordPropertyText]
        [DefaultValue("Password123!")]
        public string confirm_password { get; set; }
        [Required]
        [MaxLength(250)]
        public string name { get; set; }
    }
}
