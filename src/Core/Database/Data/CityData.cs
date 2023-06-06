using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_server.src.Core.Database.Data
{
    [Table("cities")]
    public class CityData
    {
        [Key]
        public int id { get; set; }
        [StringLength(250)]
        public string name { get; set; }
        public virtual ICollection<DistrictData> districts { get; set; }
        public CityData()
        {
            districts = new List<DistrictData>();
        }
    }
}
