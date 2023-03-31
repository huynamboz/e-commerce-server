using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.Src.Core.Modules.Auth.Dto
{
    public class registerModel
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [MaxLength(1000)]
        [PasswordPropertyText]
        public string password { get; set; }
        [Required]
        [MaxLength(250)]
        public string name { get; set; }


    }
}
