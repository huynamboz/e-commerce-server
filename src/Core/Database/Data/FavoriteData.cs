using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e_commerce_server.src.Core.Database.Data
{
    [Table("favorites")]
    public class FavoriteData
    {
        public int product_id { get; set; }
        public int user_id { get; set; }
        public DateTime create_at {  get; set; }
        public virtual ProductData product { get; set; }
        public virtual UserData user { get; set; }

    }
}