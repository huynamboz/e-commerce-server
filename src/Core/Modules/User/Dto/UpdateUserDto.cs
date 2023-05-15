using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.src.Core.Modules.User.Dto
{
    public class UpdateUserDto
    {
        [StringLength(250)]
        [EmailAddress]
        public string email { get; set; }
        [MaxLength(250)]
        public string name { get; set; }
        [RegularExpression(@"(84|0[3|5|7|8|9])+([0-9]{8})\b",
        ErrorMessage = "Số điện thoại không hợp lệ")]
        [DefaultValue("0812345678")]
        public string? phone_number { get; set; }
        public string? address { get; set; }
        public bool? gender { get; set; }
        public DateTime? birthday { get; set; }
        [DefaultValue("550500")]
        public int? district_id { get; set; }
        public string? avatar { get; set; }
    }
}
