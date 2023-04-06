using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Modules.Product.Dto;
using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Core.Modules.User;
using e_commerce_server.Src.Packages.HttpException;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_server.src.Core.Modules.Product
{
    public class ProductRepository
    {
        private readonly MyDbContext _context;
        private UserRepository userRepository;
        public ProductRepository(MyDbContext context)
        {
            _context = context;
            userRepository = new UserRepository(context);
        }
        public DbSet<ProductData> GetProducts()
        {
            try
            {
                return _context.Products;
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public List<object> GetProductByPage(DbSet<ProductData> products,int page, int pageSize)
        {
            try
            {
                return products
                    .Skip((page -1) * 10)
                    .Take(pageSize)
                    .Select(
                    p => new
                    {
                        p.id,
                        p.name,
                        p.price,
                        p.discount,
                        p.description,
                        p.created_at,
                        p.updated_at,
                        p.product_status,
                        thumbnails = _context.Thumbnails.Where(cond => cond.product_id == p.id).Select(data => data.thumbnail_url).ToList(),
                        p.user_id,
                        p.category_id,
                        category = p.category.name,
                        address = _context.Districts
                            .Where(a => a.id == p.user.district_id)
                            .Select(ct => ct.city.name).SingleOrDefault()
                    }
                ).Cast<object>().ToList();
            } catch (Exception ex)
                {
                    throw new InternalException(ex.Message);
                }
        }
        public ProductData UpdateProduct(int productID, ProductDto productDto)
        {
            try
            {
                var product = _context.Products.SingleOrDefault(p => p.id == productID);

                //delete list old thumbnail of product
                var thumbnailsToDelete = _context.Thumbnails.Where(cond => cond.product_id == productID).ToList();

                _context.Thumbnails.RemoveRange(thumbnailsToDelete);

                //add new list thumbnail for product
                List<ThumbnailData> thumbnails = new List<ThumbnailData>();

                foreach (var thumbnail in productDto.thumbnails) {
                    ThumbnailData item = new ThumbnailData
                    {
                        thumbnail_url = thumbnail,
                        product_id = productID,
                    };

                    thumbnails.Add(item);
                }
                _context.AddRange(thumbnails);

                //update detail 
                product.description = productDto.description;
                product.price = productDto.price;
                product.name = productDto.name;
                product.updated_at = DateTime.Now;
                product.category_id = productDto.category_id;
                product.discount = productDto.discount;

                _context.SaveChanges();

                return product;
            } catch (Exception ex)
                {
                    throw new InternalException(ex.Message);
            }
        }
        public ProductData AddProduct(ProductDto productDto,int idUser)
        {
            try
            {
                var newProduct = new ProductData
                {
                    name = productDto.name,
                    description = productDto.description,
                    price = productDto.price,
                    discount = productDto.discount,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now,
                    product_status = productDto.product_status,
                    user_id = idUser,
                    category_id = productDto.category_id,
                };

                _context.Add(newProduct);
                _context.SaveChanges();

                foreach (var thumbnail in productDto.thumbnails)
                {
                    ThumbnailData item = new ThumbnailData
                    {
                        thumbnail_url = thumbnail,
                        product_id = newProduct.id,
                    };

                    _context.Add(item);
                }
                
                _context.SaveChanges();

                return newProduct;
            }
            catch (Exception e)
            {
                throw new InternalException(e.Message);
            }
        }
        public List<string> GetThumbnailsByProductId(int id)
        {
            try
            {
                return _context.Thumbnails.Where(cond => cond.product_id == id).Select(data => data.thumbnail_url).ToList();
            } catch (Exception e)
            {
                throw new InternalException(e.Message);
            }
        }
        public ProductDto? GetProductById(int id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.id == id);

                if (product == null) 
                {
                    return null;
                }

                var thumbnails = GetThumbnailsByProductId(id);

                var user = userRepository.FindById(product.user_id);

                var address = _context.Districts
                        .Where(a => a.id == user.district_id)
                        .Select(ct => ct.city.name).SingleOrDefault();

                return new ProductDto
                {
                    id = product.id,
                    name = product.name,
                    price = product.price,
                    discount = product.discount,
                    description = product.description,
                    created_at = product.created_at,
                    updated_at = product.updated_at,
                    product_status = product.product_status,
                    thumbnails = thumbnails,
                    address = address,
                    user_id = product.user.id,
                    category_id = product.category_id,
                };
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
    }
}
