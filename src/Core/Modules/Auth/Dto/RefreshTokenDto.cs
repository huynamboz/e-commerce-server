using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.src.Core.Modules.Auth.Dto
{
    public class RefreshTokenDto
    {
        [Required]
        public string refresh_token { get; set; }
    }
}
