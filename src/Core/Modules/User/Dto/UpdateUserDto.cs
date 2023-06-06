using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using e_commerce_server.src.Core.Common.Enum;
using e_commerce_server.src.Core.Utils;

namespace e_commerce_server.src.Core.Modules.User.Dto
{
    public class UpdateUserDto
    {
        [StringLength(250)]
        [EmailAddress]
        [RegularExpression(Regex.EMAIL, ErrorMessage = InterceptorEnum.INVALID_EMAIL)]
        public string? email { get; set; }
        [MaxLength(250)]
        public string? name { get; set; }
        [RegularExpression(Regex.PHONE_NUMBER, ErrorMessage = InterceptorEnum.INVALID_PHONE_NUMBER)]
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
