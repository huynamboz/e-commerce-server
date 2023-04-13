using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Modules.Media.Service;
using e_commerce_server.src.Core.Modules.Product.Dto;
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
        private readonly UserRepository userRepository;
        private readonly MediaService mediaService;
        public ProductRepository(MyDbContext context)
        {
            _context = context;
            userRepository = new UserRepository(context);
            mediaService = new MediaService();
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
                    .Select( product => new 
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
        public ProductData UpdateProduct(List<string> filePaths, int productId, AddProductDto productDto)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var product = _context.Products.SingleOrDefault(p => p.id == productId);

                if (product == null)
                {
                    throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
                }

                //delete old product's thumbnails
                var thumbnailsToDelete = _context.Thumbnails.Where(cond => cond.product_id == productId).ToList();

                _context.Thumbnails.RemoveRange(thumbnailsToDelete);

                //add new list thumbnail for product
                List<ThumbnailData> thumbnails = new List<ThumbnailData>();

                for(int i = 0; i < filePaths.Count; i++)
                {
                    string thumbnailUrl = mediaService.UploadOne(filePaths[i], $"BadSupermarket/users/{product.user_id}/products/{productId}", Convert.ToString(i + 1));

                    ThumbnailData item = new ThumbnailData
                    {
                        thumbnail_url = thumbnailUrl,
                        product_id = productId,
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

                transaction.Commit();

                return product;
            } catch (Exception ex)
            {
                transaction.Rollback();
                throw new InternalException(ex.Message);
            }
        }
        public ProductData AddProduct(List<string> filePaths, AddProductDto productDto,int userId)
        {
            using var transaction = _context.Database.BeginTransaction();

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
                    status_id = productDto.status_id,
                    user_id = userId,
                    category_id = productDto.category_id,
                };

                _context.Add(newProduct);
                _context.SaveChanges();

                for(int i = 0; i < filePaths.Count; i++)
                {
                    string thumbnailUrl = mediaService.UploadOne(filePaths[i], $"BadSupermarket/users/{userId}/products/{newProduct.id}", Convert.ToString(i + 1));
                    ThumbnailData item = new ThumbnailData
                    {
                        thumbnail_url = thumbnailUrl,
                        product_id = newProduct.id,
                    };

                    _context.Add(item);
                }

                _context.SaveChanges();

                transaction.Commit();

                return newProduct;
            }
            catch (Exception e)
            {
                transaction.Rollback();
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
        public object? GetProductById(int id)
        {
            try
            {
                var product = _context.Products
                    .Include(p => p.thumbnails)
                    .Include(p => p.user).ThenInclude(u => u.district).ThenInclude(d => d.city)
                    .Include(p => p.category)
                    .Include(p => p.product_status)
                    .FirstOrDefault(p => p.id == id);

                if (product == null)
                {
                    return null;
                }

                return new
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
                };
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
    }
}
