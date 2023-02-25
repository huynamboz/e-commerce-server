using e_commerce_server.Modes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace e_commerce_server.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext (DbContextOptions options) : base(options) { }
        #region
        public DbSet<HangHoa> HangHoas { get; set;}
        public DbSet<Loai> Loais { get; set;}
        public DbSet<FileModel> Files { get; set;}

        #endregion

    }
    //public class YourDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    //{
    //    public MyDbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
    //        optionsBuilder.UseSqlServer("Data Source=HUYNAMVN\\SQLEXPRESS;Initial Catalog=eCommerce;Integrated Security=true;TrustServerCertificate=True");

    //        return new MyDbContext(optionsBuilder.Options);
    //    }
    //}
}
