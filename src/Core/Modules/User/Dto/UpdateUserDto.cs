using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.src.Core.Modules.User.Dto
{
    public class UpdateUserDto
    {
        [StringLength(250)]
        [EmailAddress]
        public string email { get; set; }
        [MaxLength(250)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Mật khẩu phải chứa ít nhất 8 kí tự và có ít nhất 1 ký tự hoa, 1 kí tự thường, 1 chữ số và 1 kí tự đặc biệt.")]
        public string password { get; set; }
        [MaxLength(250)]
        public string name { get; set; }
        [RegularExpression(@"(84|0[3|5|7|8|9])+([0-9]{8})\b",
        ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? phone_number { get; set; }
        public string? address { get; set; }
        public bool? gender { get; set; }
        public DateTime? birthday { get; set; }
        public int? district_id { get; set; }
    }
}
