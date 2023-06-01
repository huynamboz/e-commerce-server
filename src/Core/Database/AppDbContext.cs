using e_commerce_server.src.Core.Config;
using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Database.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace e_commerce_server.src.Core.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        #region
        public virtual DbSet<UserData> Users { get; set; }
        public virtual DbSet<CityData> Cities { get; set; }
        public virtual DbSet<DistrictData> Districts { get; set; }
        public virtual DbSet<ProductData> Products { get; set; }
        public virtual DbSet<CategoryData> Categories { get; set; }
        public virtual DbSet<ProductStatusData> ProductStatuses { get; set; }
        public virtual DbSet<ThumbnailData> Thumbnails { get; set; }
        public virtual DbSet<FavoriteData> Favorites { get; set; }
        public virtual DbSet<ReviewData> Reviews { get; set; }
        public virtual DbSet<ReportData> Reports { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            string filePath = FileConfig.ApplyFileProviderPath() + "/json/locations.json";

            string jsonString = File.ReadAllText(filePath);

            List<LocationModel> cities = JsonConvert.DeserializeObject<List<LocationModel>>(jsonString) ?? new List<LocationModel>();

            foreach(var city in cities) {
                modelBuilder.Entity<CityData>().HasData(
                    new CityData
                    {
                        id = city.id,
                        name = city.name,
                    }
                );

                foreach (var district in city.districts)
                {
                    modelBuilder.Entity<DistrictData>().HasData(
                        new DistrictData
                        {
                            id = district.id,
                            name = district.name,
                            city_id = city.id,
                        }
                    );
                }
            }

            modelBuilder.Entity<CategoryData>().HasData(
                new CategoryData[]
                {
                    new CategoryData
                    {
                        id = 1,
                        name = "Điện thoại",
                        thumbnail = "https://cdn11.dienmaycholon.vn/filewebdmclnew/public/userupload/files/mtsp/di-dong/lua-chon-mua-dien-thoai-di-dong-phu-hop.jpg"
                    },
                    new CategoryData
                    {
                        id = 2,
                        name = "Máy tính & laptop",
                        thumbnail = "https://phuonghoangtv.com/wp-content/uploads/2020/09/nen-mua-lap-top-hang-nao-tot.jpg"
                    },
                    new CategoryData
                    {
                        id = 3,
                        name = "Máy ảnh & máy quay",
                        thumbnail = "https://images2.thanhnien.vn/528068263637045248/2023/5/25/canon-eos-r100-1684979537624432755196.jpg"
                    },
                    new CategoryData
                    {
                        id = 4,
                        name = "Đồng hồ",
                        thumbnail = "https://product.hstatic.net/1000223154/product/z3618907692798_de826ce083ece5970076a2f86e1fd7f8__1__095576c58e414d51aca229831f895982_master.jpg"
                    },
                    new CategoryData
                    {
                        id = 5,
                        name = "Giày dép nam",
                        thumbnail = "https://salt.tikicdn.com/cache/w1200/ts/product/56/96/c3/a8037ee5af3afe4df0ed8fabbde729d7.jpg"
                    },
                    new CategoryData
                    {
                        id = 6,
                        name = "Tablet",
                        thumbnail = "https://cdn.mos.cms.futurecdn.net/LTkjzWfU6NNdMd4EibvpDV.jpg"
                    },
                    new CategoryData
                    {
                        id = 7,
                        name = "Đồ thể thao & du lịch",
                        thumbnail = "https://media3.scdn.vn/img4/2023/03_24/JdIylfqMqfcmckdhhhzT.jpg"
                    },
                    new CategoryData
                    {
                        id = 8,
                        name = "Ô tô & xe máy & xe đạp",
                        thumbnail = "https://cdnimg.vietnamplus.vn/uploaded/qrndqxjwp/2022_01_15/5c13378d057c480a8e98c64a413e20613682161636517973.jpg"
                    },
                    new CategoryData
                    {
                        id = 9,
                        name = "Balo & túi ví nam",
                        thumbnail = "https://agiare.vn/wp-content/uploads/2020/01/vi-da-bo-nam.jpg"
                    },
                    new CategoryData
                    {
                        id = 10,
                        name = "Đồ chơi",
                        thumbnail = "https://www.moby.com.vn/data/bt8/do-choi-xep-hinh-tuoi-tho-110-chi-tiet-4_1644893165.png"
                    },
                    new CategoryData
                    {
                        id = 11,
                        name = "Đồ cho thú cưng",
                        thumbnail = "https://vuongquocloaivat.com/wp-content/uploads/2020/06/banh-donut-cho-thu-cung-compressed.jpg"
                    },
                    new CategoryData
                    {
                        id = 12,
                        name = "Dụng cụ và thiết bị",
                        thumbnail = "https://nikatei.com.vn/image/catalog/do-dien-tu.jpg"
                    },
                    new CategoryData
                    {
                        id = 13,
                        name = "Thời trang nữ",
                        thumbnail = "https://image-us.eva.vn/upload/1-2021/images/2021-02-26/min-house---shop-thoi-trang-nu-duoc-yeu-thich-thoi-trang-nu-1-1614307605-24-width600height600.jpg"
                    },
                    new CategoryData
                    {
                        id = 14,
                        name = "Mẹ và bé",
                        thumbnail = "https://thuthuatnhanh.com/wp-content/uploads/2022/06/Anh-me-va-be.jpg"
                    },
                    new CategoryData
                    {
                        id = 15,
                        name = "Nhà cửa và đời sống",
                        thumbnail = "https://thietkenoithat.com/Portals/0/xNews/uploads/2018/6/20/nha-dep-1.jpg"
                    },
                    new CategoryData
                    {
                        id = 16,
                        name = "Mỹ phẩm",
                        thumbnail = "https://cdn.tgdd.vn/Files/2021/10/08/1388863/my-pham-thuan-chay-my-pham-sach-va-my-pham-huu-co-co-phai-la-mot-202110082321000878.jpg"
                    },
                    new CategoryData
                    {
                        id = 17,
                        name = "Sức khỏe",
                        thumbnail = "https://suckhoedoisong.qltns.mediacdn.vn/324455921873985536/2022/8/30/uong-thuoc-thoi-diem-nao-16618466465071926930931.png"
                    },
                    new CategoryData
                    {
                        id = 18,
                        name = "Giày dép nữ",
                        thumbnail = "https://img.mwc.com.vn/giay-thoi-trang?w=480&h=510&FileInput=//Upload/2022/08/z3682162219081-b6287f86835b1a448ea12308528ea450.jpg"
                    },
                    new CategoryData
                    {
                        id = 19,
                        name = "Túi ví nữ",
                        thumbnail = "https://mikaystore.r.worldssl.net/wp-content/uploads/2021/03/tui-xach-nu-thich-hop-mang-di-su-kien.png"
                    },
                    new CategoryData
                    {
                        id = 20,
                        name = "Sách báo",
                        thumbnail = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQVaOQolKo7VhPR_egYxV_QQxgLko8BBZnfEQ&usqp=CAU"
                    },
                    new CategoryData
                    {
                        id = 21,
                        name = "Thời trang cho bé",
                        thumbnail = "https://salt.tikicdn.com/cache/w1200/ts/product/8a/a4/19/22f686157264d6a2b8cdc1511997c5ca.jpg"
                    },
                    new CategoryData
                    {
                        id = 22,
                        name = "Giặt giũ",
                        thumbnail = "https://cdn.tgdd.vn/Files/2015/10/17/722583/su-dung-bot-giat-cho-may-giat-nhu-the-nao-dung-cach-8.jpg"
                    },
                    new CategoryData
                    {
                        id = 23,
                        name = "Thiết bị điện gia dụng",
                        thumbnail = "https://www.dienlamhong.com/wp-content/uploads/2021/02/cong-ty-thiet-bi-dien-lam-hong.png"
                    },
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
                    description = "string",
                    price = 120000,
                    discount = 10,
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

            modelBuilder.Entity<UserData>()
                .HasOne(u => u.district)
                .WithMany(r => r.users)
                .HasForeignKey(u => u.district_id);

            modelBuilder.Entity<UserData>(entity =>
            {
                entity.HasIndex(e => e.email).IsUnique();
                entity.HasIndex(e => e.phone_number).IsUnique();
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

            modelBuilder.Entity<FavoriteData>()
                .HasKey(e => new { e.product_id, e.user_id });

            modelBuilder.Entity<FavoriteData>()
                .HasOne(p => p.product)
                .WithMany(p => p.favorites)
                .HasForeignKey(p => p.product_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FavoriteData>()
                .HasOne(p => p.user)
                .WithMany(p => p.favorites)
                .HasForeignKey(p => p.user_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReviewData>()
                .HasOne(p => p.product)
                .WithMany(p => p.reviews)
                .HasForeignKey(p => p.product_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReviewData>()
                .HasKey(e => new { e.product_id, e.user_id });

            modelBuilder.Entity<ReviewData>()
                .HasOne(p => p.user)
                .WithMany(p => p.reviews)
                .HasForeignKey(p => p.user_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReportData>()
                .HasOne(p => p.product)
                .WithMany(p => p.reports)
                .HasForeignKey(p => p.product_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReportData>()
                .HasKey(e => new { e.product_id, e.user_id });

            modelBuilder.Entity<ReportData>()
                .HasOne(p => p.user)
                .WithMany(p => p.reports)
                .HasForeignKey(p => p.user_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserData>().HasData(
                new UserData[]
                {
                    new UserData
                    {
                        id = 1,
                        name = "Admin",
                        email = "user@example.com",
                        password = "$2a$04$GmL6XUWBFM9nSUzBynCNa.nvLo7pfiPK9sg1tdNiF3tKmhoMP1MIi", //Password123!
                        role_id = 1,
                        phone_number = "0812345678",
                        address = "54 Nguyễn Lương Bằng",
                        gender = true,
                        district_id = 550500,
                        avatar = "https://res.cloudinary.com/dgtaa84en/image/upload/v1678819995/organizations/116/avatar/avatar.jpg",
                        active_status = true,
                    },
                    new UserData
                    {
                        id = 2,
                        name = "User",
                        email = "user2@example.com",
                        password = "$2a$04$GmL6XUWBFM9nSUzBynCNa.nvLo7pfiPK9sg1tdNiF3tKmhoMP1MIi", //Password123!
                        role_id = 2,
                        active_status = false
                    },
                }
            );
        }
    }
}
