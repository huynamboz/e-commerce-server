using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_server.src.Core.Database.Data
{
    [Table("districts")]
    public class DistrictData
    {
        [Key]
        public int id { get; set; }
        [StringLength(250)]
        public string name { get; set; }
        public int city_id { get; set; }
        public virtual CityData city { get; set; }
        public virtual ICollection<UserData> users { get; set; }
        public DistrictData()
        {
            users = new List<UserData>();
        }
    }
}
