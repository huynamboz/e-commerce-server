using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_server.src.Core.Database.Data
{
    [Table("users")]
    public class UserData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [StringLength(250)]
        public string email { get; set; }
        [MaxLength(250)]
        public string password { get; set; }
        [MaxLength(250)]
        public string name { get; set; }
        public int role_id { get; set; }
        [StringLength(20)]
        public string? phone_number { get; set; }
        public string? address { get; set; }
        public bool? gender { get; set; }
        public DateTime? birthday { get; set; }
        public string? avatar { get; set; }
        public string? reset_token { get; set; }
        public DateTime? reset_token_expiration_date { get; set; }
        public DateTime created_at { get; set; }
        public DateTime update_at { get; set; }
        public DateTime? delete_at { get; set; }
        public bool active_status { get; set; }
        public int? district_id { get; set; }
        public virtual DistrictData district { get; set; }
        public string? refresh_token { get; set; }
        public int report_count { get; set; }
        public virtual ICollection<ProductData> products { get; set; }
        public virtual ICollection<FavoriteData> favorites { get; set; }
        public virtual ICollection<ReviewData> reviews { get; set; }
        public virtual ICollection<ReportData> reports { get; set; }
        public UserData()
        {
            this.products = new List<ProductData>();
            this.favorites = new List<FavoriteData>();
            this.reviews = new List<ReviewData>();
            this.reports = new List<ReportData>();
        }
    }
}
