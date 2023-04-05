using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.Src.Core.Modules.Auth.Dto
{
    public class RefreshTokenDto
    {
        [Required]
        [DefaultValue("refresh_token")]
        public string grant_type { get; set; }
        [Required]
        public string refresh_token { get; set; }
    }
}
