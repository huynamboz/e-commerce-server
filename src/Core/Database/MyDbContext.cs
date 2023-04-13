using e_commerce_server.src.Core.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_server.src.Core.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }
        #region
        public virtual DbSet<UserData> Users { get; set; }
        public virtual DbSet<CityData> Cities { get; set; }
        public virtual DbSet<DistrictData> Districts { get; set; }
        public virtual DbSet<ProductData> Products { get; set; }
        public virtual DbSet<CategoryData> Categories { get; set; }
        public virtual DbSet<ProductStatusData> ProductStatuses { get; set; }
        public virtual DbSet<ThumbnailData> Thumbnails { get; set; }
        public virtual DbSet<FavoriteData> Favorites { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Write Fluent API configurations here

            Task.Run(async () =>
            {
                // LocationModel locationModel = new();

                // var cities = await locationModel.GetApi("https://api.goship.io/api/ext_v1/cities");

                // foreach (var city in cities)
                // {
                //     modelBuilder.Entity<CityData>().HasData(
                //         new CityData
                //         {
                //             id = city.id,
                //             name = city.name,
                //         }
                //     );

                //     var districts = await locationModel.GetApi($"https://api.goship.io/api/ext_v1/cities/{city.id}/districts");

                //     foreach (var district in districts)
                //     {
                //         modelBuilder.Entity<DistrictData>().HasData(
                //             new DistrictData
                //             {
                //                 id = district.id,
                //                 name = district.name,
                //                 city_id = city.id,
                //             }
                //         );
                //     }
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

                modelBuilder.Entity<UserData>().HasData(
                    new UserData
                    {
                        id = 1,
                        name = "John Doe",
                        email = "user@example.com",
                        password = "$2a$04$GmL6XUWBFM9nSUzBynCNa.nvLo7pfiPK9sg1tdNiF3tKmhoMP1MIi", //Password123!
                        role_id = 1,
                    }
                );
            }).GetAwaiter().GetResult();
        }
    }
}
