using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.src.Core.Modules.User.Dto
{
    public class UpdateUserDto
    {
        [StringLength(250)]
        [EmailAddress]
        public string email { get; set; }
        [MaxLength(250)]
        public string password { get; set; }
        [MaxLength(250)]
        public string name { get; set; }
        public string? phone_number { get; set; }
        public string? address { get; set; }
        public bool? gender { get; set; }
        public DateTime? birthday { get; set; }
        public string? avatar { get; set; }
        public int? district_id { get; set; }
    }
}
