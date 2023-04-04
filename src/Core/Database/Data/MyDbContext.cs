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
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here
            base.OnModelCreating(modelBuilder);

            LocationModel locationModel = new LocationModel();

            //Task.Run(async () =>
            //{
            //    var cities = await locationModel.GetApi("https://api.goship.io/api/ext_v1/cities");

            //    foreach (var city in cities)
            //    {
            //        modelBuilder.Entity<CityData>().HasData(
            //            new CityData
            //            {
            //                id = city.id,
            //                name = city.name,
            //            }
            //        );

            //        var districts = await locationModel.GetApi($"https://api.goship.io/api/ext_v1/cities/{city.id}/districts");
                        
            //        foreach( var district in districts)
            //        {
            //            modelBuilder.Entity<DistrictData>().HasData(
            //                new DistrictData
            //                {
            //                    id = district.id,
            //                    name = district.name,
            //                    city_id = city.id,
            //                }
            //            );
            //        }
            //    }

            //    modelBuilder.Entity<UserData>().HasData(
            //        new UserData
            //        {
            //            id = 1,
            //            name = "John Doe",
            //            email = "string@gmail.com",
            //            password = "string",
            //            active_status = false,
            //            role_id = 1,
            //        }
            //    );
            //    modelBuilder.Entity<DistrictData>()
            //        .HasOne(u => u.city)
            //        .WithMany(r => r.districts)
            //        .HasForeignKey(u => u.city_id);

            //    modelBuilder.Entity<UserData>(entity =>
            //    {
            //        entity.HasIndex(e => e.email).IsUnique();

            //    });
            //}).GetAwaiter().GetResult();
        }
    }
}
