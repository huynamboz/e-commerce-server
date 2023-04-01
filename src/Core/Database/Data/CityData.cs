using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_server.Src.Core.Database.Data
{
    [Table("cities")]
    public class CityData
    {
        [Key]
        public int id { get; set; }
        [StringLength(250)]
        public string name { get; set; }
        public List<DistrictData> districts { get; set; }
    }
}
