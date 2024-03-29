﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_server.src.Core.Database.Data
{
    [Table("thumbnails")]
    public class ThumbnailData
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string thumbnail_url { get; set; }
        [Required]
        public int product_id { get; set; }
        public virtual ProductData product { get; set; }
    }
}
