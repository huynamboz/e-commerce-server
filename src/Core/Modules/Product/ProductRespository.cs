﻿using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Common.Enum;
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
        public List<ProductData> GetProducts()
        {
            try
            {
                return _context.Products.Where(p => p.user.active_status == true && p.delete_at == null).ToList();
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
                return _context.Products.Where(p => p.user_id == userId && p.user.active_status == true && p.delete_at == null).ToList();
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public List<object> GetProductsByPage(int page)
        {
            try
            {
                return _context.Products
                    .Where(p => p.user.active_status == true && p.delete_at == null)
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
                            product.user.avatar,
                            location = Convert.ToBoolean(product.user.district_id) ? $"{product.user.district.name}, {product.user.district.city.name}" : null
                        },
                        thumbnails = product.thumbnails.Select(t => t.thumbnail_url),
                        category = product.category.name,
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
                return _context.Products
                    .Include(p => p.thumbnails)
                    .Include(p => p.user).ThenInclude(u => u.district).ThenInclude(d => d.city)
                    .Include(p => p.category)
                    .Include(p => p.product_status)
                    .SingleOrDefault(p => p.user_id == userId && p.id == productId && p.user.active_status && p.delete_at == null);
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
                    .Where(p => p.user_id == userId && p.user.active_status == true && p.delete_at == null)
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
                            product.user.avatar,
                            location = Convert.ToBoolean(product.user.district_id) ? $"{product.user.district.name}, {product.user.district.city.name}" : null
                        },
                        thumbnails = product.thumbnails.Select(t => t.thumbnail_url),
                        category = product.category.name,
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
        public void DeleteProduct(ProductData product)
        {
            try
            {
                product.delete_at = DateTime.Now;

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new InternalException(e.Message);
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
                    .FirstOrDefault(p => p.id == id && p.user.active_status && p.delete_at == null);
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public void AddProductToFavorite(FavoriteData favorite)
        {
            try
            {
                _context.Favorites.Add(favorite);
                _context.SaveChanges();
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public List<FavoriteData> GetFavoriteProductsByUserId(int userId)
        {
            try
            {
                return _context.Favorites.Where(p => p.user_id == userId && p.user.active_status).ToList();
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public List<object> GetFavoriteProductsByUserIdByPage(int page, int userId)
        {
            try
            {
                return _context.Favorites
                    .Where(p => p.user_id == userId && p.product.user.active_status && p.product.delete_at == null)
                    .Skip((page -1) * 10)
                    .Take(PageSizeEnum.PAGE_SIZE)
                    .Include(p => p.product).ThenInclude(p => p.category)
                    .Include(p => p.product).ThenInclude(p => p.product_status)
                    .Select(p => new
                    {
                        p.product.id,
                        p.product.name,
                        p.product.price,
                        p.product.discount,
                        p.product.description,
                        p.product.created_at,
                        p.product.updated_at,
                        p.product.product_status.status,
                        user = new
                        {
                            p.product.user.id,
                            p.product.user.name,
                            p.product.user.phone_number,
                            p.product.user.avatar,
                            location = Convert.ToBoolean(p.user.district_id) ? $"{p.user.district.name}, {p.user.district.city.name}" : null
                        },
                        thumbnails = p.product.thumbnails.Select(t => t.thumbnail_url),
                        category = p.product.category.name,
                    }
                ).Cast<object>().ToList();
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public FavoriteData? GetFavoriteProductByUserIdAndProductId(int userId, int productId)
        {
            try
            {
                return _context.Favorites.SingleOrDefault(p => p.user_id == userId && p.product_id == productId && p.product.delete_at == null);
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public void RemoveProductFromFavorite(FavoriteData favorite)
        {
            try
            {
                _context.Favorites.Remove(favorite);
                _context.SaveChanges();
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
    }
}