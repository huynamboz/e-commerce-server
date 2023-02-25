using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.Modes
{
    public class productModels
    {
        public string TenHh { get; set; }
        public string MoTa { get; set; }
        [Range(0, double.MaxValue)]
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }
        public int? MaLoai { get; set; }
    }
}
