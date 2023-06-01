using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.src.Core.Modules.Product.Dto
{
    public class AddCategoryDto
    {
        [StringLength(50)]
        [Required]
        public string name { get; set; }
    }
}
