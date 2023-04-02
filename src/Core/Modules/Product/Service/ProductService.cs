using e_commerce_server.src.Core.Modules.Product.Dto;
using e_commerce_server.Src.Core.Database.Data;

namespace e_commerce_server.src.Core.Modules.Product.Service
{
    public class ProductService
    {
        readonly MyDbContext _dbContext;
        public ProductService(MyDbContext context) {
            _dbContext= context;
        }
        private string getCity(int user)
        {
            var districtID = _dbContext.Districts.Where(a => a.id == user).Select(
                             ct => ct.city.name
                             ).SingleOrDefault();
            return districtID.ToString();
        }
        public object GetAllProduct()
        {
            var listProduct = _dbContext.Products.Select(
                p => new
                {
                    p.id, p.name, p.price, p.discount, p.description, p.created_at, p.updated_at,
                    p.thumbnail_url, p.active_status, p.product_status,
                    p.keyword, p.user_id, p.category_id,
                    category = p.category.name,
                    address = _dbContext.Districts.Where(a => a.id == p.user.district_id).Select(
                             ct => ct.city.name
                             ).SingleOrDefault()
        }
                ).ToList();

            return new
            {
                success = true,
                data = listProduct
            };
        }
    }
}
