using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace e_commerce_server.src.Core.Database.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }
        #region
        public DbSet<UserData> Users { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserData>().HasData(
                new UserData
                {
                    id = 1,
                    name = "John Doe",
                    email = "test@gmail.com",
                    password = "string",
                    active_status = false,
                    role_id = 1,
                }
            );
            //Property Configurations
            modelBuilder.Entity<UserData>(entity =>
            {
                entity.HasIndex(e => e.id).IsUnique();
                entity.Property(e => e.name).IsRequired().HasMaxLength(250);
                entity.Property(e => e.email).IsRequired().HasMaxLength(250);

            });
        }
    }
}
