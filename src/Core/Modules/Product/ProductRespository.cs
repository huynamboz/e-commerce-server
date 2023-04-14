using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Modules.Media.Service;
using e_commerce_server.src.Core.Modules.Product.Service;
using e_commerce_server.src.Core.Common.Enum;
using e_commerce_server.src.Core.Modules.User;
using e_commerce_server.src.Packages.HttpExceptions;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_server.src.Core.Modules.Product
{
    public class ProductRepository
    {
        private readonly MyDbContext _context;
        public ProductRepository(MyDbContext context)
        {
            _context = context;
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
        public List<ProductData> GetProductsByUserId(int userId)
        {
            try
            {
                return _context.Products.Where(p => p.user_id == userId).ToList();
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public List<object> GetProductsByPage(DbSet<ProductData> products, int page)
        {
            try
            {
                return _context.Products
                    .Skip((page -1) * 10)
                    .Take(PageSizeEnum.PAGE_SIZE)
                    .Include(p => p.thumbnails)
                    .Include(p => p.user).ThenInclude(u => u.district).ThenInclude(d => d.city)
                    .Include(p => p.category)
                    .Include(p => p.product_status)
                    .Select(product => new 
                    {
                        product.id,
                        product.name,
                        product.price,
                        product.discount,
                        product.description,
                        product.created_at,
                        product.updated_at,
                        product.product_status.status,
                        user = new
                        {
                            product.user.id,
                            product.user.name,
                            product.user.phone_number,
                            product.user.avatar
                        },
                        thumbnails = product.thumbnails.Select(t => t.thumbnail_url),
                        category = product.category.name,
                        location = $"{product.user.district.name}, {product.user.district.city.name}"
                    }).Cast<object>().ToList();
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public ProductData? GetProductByIdAndUserId(int userId, int productId)
        {
            try
            {
                return _context.Products.SingleOrDefault(p => p.user_id == userId && p.id == productId);
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public List<object> GetProductsByUserIdByPage(int page, int userId)
        {
            try
            {
                return _context.Products
                    .Where(p => p.user_id == userId)
                    .Skip((page -1) * 10)
                    .Take(PageSizeEnum.PAGE_SIZE)
                    .Include(p => p.thumbnails)
                    .Include(p => p.user).ThenInclude(u => u.district).ThenInclude(d => d.city)
                    .Include(p => p.category)
                    .Include(p => p.product_status)
                    .Select(product => new
                    {
                        product.id,
                        product.name,
                        product.price,
                        product.discount,
                        product.description,
                        product.created_at,
                        product.updated_at,
                        product.product_status.status,
                        user = new
                        {
                            product.user.id,
                            product.user.name,
                            product.user.phone_number,
                            product.user.avatar
                        },
                        thumbnails = product.thumbnails.Select(t => t.thumbnail_url),
                        category = product.category.name,
                        location = $"{product.user.district.name}, {product.user.district.city.name}"
                    }
                ).Cast<object>().ToList();
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public ProductData AddOrUpdateProduct(ProductData product, List<string> thumbnailUrls)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                if (product.id == 0) {
                    _context.Products.Add(product);
                    _context.SaveChanges();

                    this.AddProductThumbnails(product.id, thumbnailUrls);
                } else {
                    _context.SaveChanges();

                    this.DeleteProductThumbnails(product.id);
                    this.AddProductThumbnails(product.id, thumbnailUrls);

                    _context.Entry(product).Reference(p => p.product_status).Load();
                    _context.Entry(product).Reference(p => p.category).Load();
                }


                transaction.Commit();

                return product;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new InternalException(e.Message);
            }
        }
        public void AddProductThumbnails(int productId, List<string> thumbnailUrls) 
        {
            try
            {
                Console.WriteLine(productId);
                List<ThumbnailData> thumbnails = new List<ThumbnailData>();

                foreach(string thumbnailUrl in thumbnailUrls)
                {
                    ThumbnailData thumbnail = new ThumbnailData
                    {
                        thumbnail_url = thumbnailUrl,
                        product_id = productId,
                    };

                    thumbnails.Add(thumbnail);
                }

                _context.Thumbnails.AddRange(thumbnails);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new InternalException(e.Message);
            }
        }
        public void DeleteProductThumbnails(int productId) {
            try
            {
                var thumbnails = _context.Thumbnails.Where(t => t.product_id == productId).ToList();

                _context.Thumbnails.RemoveRange(thumbnails);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new InternalException(e.Message);
            }
        }
        public void DeleteProductById(int productId)
        {
            try
            {
                var product = _context.Products.SingleOrDefault(p => p.id == productId);

                if (product == null)
                {
                    throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
                }

                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public ProductData? GetProductById(int id)
        {
            try
            {
                return _context.Products
                    .Include(p => p.thumbnails)
                    .Include(p => p.user).ThenInclude(u => u.district).ThenInclude(d => d.city)
                    .Include(p => p.category)
                    .Include(p => p.product_status)
                    .FirstOrDefault(p => p.id == id);
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
    }
}
