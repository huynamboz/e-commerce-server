using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.Model
{
    public class LoaiModels
    {
        [Required]
        [MaxLength(255)]
        public string TenLoai { get; set; }
    }
}
