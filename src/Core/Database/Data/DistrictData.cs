using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_server.Src.Core.Database.Data
{
    [Table("districts")]
    public class DistrictData
    {
        [Key]
        public int id { get; set; }
        [StringLength(250)]
        public string name { get; set; }
        public int city_id { get; set; }
        public CityData city { get; set; }
    }
}
