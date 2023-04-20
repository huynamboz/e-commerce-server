using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_server.src.Core.Database.Data
{
    [Table("reports")]
    public class ReportData
    {
        public int product_id { get; set; }
        public int user_id { get; set; }
        public string description { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }
        public virtual ProductData product { get; set; }
        public virtual UserData user { get; set; }

    }
}
