using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.Src.Core.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_server.Src.Core.Database.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }
        #region
        public DbSet<UserData> Users { get; set; }
        public DbSet<CityData> Cities { get; set; }
        public DbSet<DistrictData> Districts { get; set; }
        public DbSet<ProductData> Products { get; set; }
        public DbSet<CategoryData> Categories { get; set; }
        #endregion
        public DbSet<ThumbnailData> Thumbnails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            LocationModel locationModel = new LocationModel();

            //Write Fluent API configurations here

            Task.Run(async () =>
            {
                // var cities = await locationModel.GetApi("https://api.goship.io/api/ext_v1/cities");

                // foreach (var city in cities)
                // {
                //    modelBuilder.Entity<CityData>().HasData(
                //        new CityData
                //        {
                //            id = city.id,
                //            name = city.name,
                //        }
                //    );

                //    var districts = await locationModel.GetApi($"https://api.goship.io/api/ext_v1/cities/{city.id}/districts");

                //    foreach (var district in districts)
                //    {
                //        modelBuilder.Entity<DistrictData>().HasData(
                //            new DistrictData
                //            {
                //                id = district.id,
                //                name = district.name,
                //                city_id = city.id,
                //            }
                //        );
                //    }
                // }

                modelBuilder.Entity<CategoryData>().HasData(
                   new CategoryData
                   {
                       id = 1,
                       name = "Đồ điện tử",
                   }
                );

                modelBuilder.Entity<ProductStatusData>().HasData(
                    new ProductStatusData[] {
                        new ProductStatusData
                        {
                            id = 1,
                            status = "Mới",
                        },
                        new ProductStatusData
                        {
                            id = 2,
                            status = "Như mới",
                        },
                        new ProductStatusData
                        {
                            id = 3,
                            status = "Đã qua sử dụng"
                        },
                        
                    }
                );

                modelBuilder.Entity<ProductData>().HasData(
                    new ProductData
                    {
                        id = 1,
                        name = "Điện thoại iphone 5",
                        description = "Chuột Gaming Không Dây LOGITECH G304 Lightspeed Chuột Chơi Game\r\n❌❌  Lưu ý: Sản phẩm là OEM, không phải hàng chính hãng! Do đó không thể kết nối với APP Logitech (OEM, viết tắt của Original Equipment Manufacturer, được hiểu là Nhà sản xuất thiết bị gốc, dùng để chỉ công ty, đối tác gia công, lắp ráp sản phẩm cho một công ty sở hữu thương hiệu và công nghệ khác). Ngoài ra đèn led của chuột cũng không mượt như đèn led chính hàng, còn lại cảm giác khi chơi game, độ bền, thông số kỹ thuật và mọi thứ khác đều rất tốt, phù hợp cho các bạn không muốn bỏ ra một khoản tiền lớn mà vẫn được trải nghiệm cảm giác chơi game tuyệt vời của chuột G304.\r\n- Bảo Hành 12 tháng cho sản Phẩm nhằm tạo uy tín và chất lượng của shop\r\n- 1 ĐỔI 1 trong vòng 07 ngày nếu có lỗi từ NSX\r\n- G304 là chuột chơi game không dây LIGHTSPEED được thiết kế cho hiệu suất thực sự với các đột phá công nghệ mới nhất ở mức giá thành phù hợp. Đó là chơi game không dây thế hệ mới, hiện đã sẵn sàng cho mọi game thủ.\r\n-Các phím chính của G304, cả ở bên trái và phải, được đánh giá 10 triệu lần nhấp. G304 cũng có nút giữa,\r\n- Nó đem lại tới 250 giờ hoạt động chỉ trên một quả pin AA\r\n\r\nThông số kĩ thuật\r\n- Độ phân giải: 200 - 12.000 DPI\r\n- Làm mịn/tăng tốc/lọc\r\n- Tăng tốc tối đa: > 40 G5\r\n- Tốc độ tối đa: > 400 IPS6\r\n- Kết nối: USB\r\n- Tốc độ báo cáo không dây: 1000 Hz (1ms)\r\n- Công nghệ không dây: LIGHTSPEED không dây\r\n- Chiều cao: 116,6 mm\r\n- Chiều rộng: 62,15 mm\r\n- Chiều dày: 38,2 mm\r\n- Trọng lượng: 99 g\r\n- Tuổi thọ PIN: 250 giờ\r\n- Sản phẩm là Hàng Công ty loại 1 cần hỗ trợ liên hệ shop nhé !!\r\n-CAM KẾT--------> BẢO HÀNH 6 THÁNG - 1 ĐỔI 1 TRONG VÒNG 3 NGÀY NẾU DO LỖI CỦA NHÀ SẢN XUẤT\r\n-Chuột Gaming Không Dây LOGITECH G304 Lightspeed Chuột Chơi Game kiểm tra kỹ lưỡng trước khi gửi đi nhằm tránh sản phẩm lỗi đến tay khách hàng\r\n- Quy trình đóng gói cẩn thận bằng túi bóng khí chống sốc nhằm hạn chế tối đa trường hợp lỗi do quá trình vận chuyển.\r\nBộ phận Kĩ Thuật sẽ kiểm tra trước khi gửi đi cho quý khách \r\n\r\n#chuotgaming #chuotgame #gaming #game #chuotchoigame #chuotkhongday #chuotUSB #chuotchuyengame #chuotlogitech #chuotsieure #G304 #chuotlogitech304 #chuotkhongday #chuotUSB #chuotchuyengame #chuotlogitech #chuotsieure #chuotG304",
                        price = 120000,
                        discount = 10,
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now,
                        status_id = 1,
                        user_id = 1,
                        category_id = 1
                    }
                );

                modelBuilder.Entity<ThumbnailData>().HasData(
                    new ThumbnailData[] {
                        new ThumbnailData
                        {
                            id = 1 ,
                            product_id = 1,
                            thumbnail_url = "https://cdn.chotot.com/9Z0aQLnCvRxd1GweQgBNy_cmjRMix_mM54sVi9Aazzs/preset:view/plain/ccd2bbdb8b1a2ec046d397c7202c7052-2819561496186255487.jpg"
                        },
                        new ThumbnailData
                        {
                            id = 2,
                            product_id = 1,
                            thumbnail_url = "https://cdn.chotot.com/9Z0aQLnCvRxd1GweQgBNy_cmjRMix_mM54sVi9Aazzs/preset:view/plain/ccd2bbdb8b1a2ec046d397c7202c7052-2819561496186255487.jpg"
                        }
                    }
                );

                modelBuilder.Entity<DistrictData>()
                    .HasOne(u => u.city)
                    .WithMany(r => r.districts)
                    .HasForeignKey(u => u.city_id);

                modelBuilder.Entity<UserData>(entity =>
                {
                    entity.HasMany(u => u.products);
                    entity.HasIndex(e => e.email).IsUnique();
                });

                modelBuilder.Entity<ProductData>()
                    .HasOne(p => p.category)
                    .WithMany(c => c.products)
                    .HasForeignKey(p => p.category_id);

                modelBuilder.Entity<ProductData>()
                    .HasOne(p => p.product_status)
                    .WithMany(s => s.products)
                    .HasForeignKey(p => p.status_id);

                modelBuilder.Entity<ProductData>()
                    .HasOne(p => p.user)
                    .WithMany(u => u.products)
                    .HasForeignKey(p => p.user_id)
                    .HasPrincipalKey(u => u.id);

                modelBuilder.Entity<ThumbnailData>()
                    .HasOne(p => p.product)
                    .WithMany(p => p.thumbnails)
                    .HasForeignKey(p => p.product_id);

                modelBuilder.Entity<UserData>().HasData(
                    new UserData
                    {
                        id = 1,
                        name = "John Doe",
                        email = "string@gmail.com",
                        password = "string",
                        role_id = 1,
                    }
                );
            }).GetAwaiter().GetResult();
        }
    }
}
