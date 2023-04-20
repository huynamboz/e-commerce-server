using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_server.src.Core.Database.Data
{
    [Table("reviews")]
    public class ReviewData
    {
        public int product_id { get; set; }
        public int user_id { get; set; }
        public int rating { get; set; }
        public string? comment { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }
        public virtual ProductData product { get; set; }
        public virtual UserData user { get; set; }
    }
}
