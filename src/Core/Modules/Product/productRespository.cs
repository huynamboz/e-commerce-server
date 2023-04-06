﻿using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Modules.Product.Dto;
using e_commerce_server.src.Core.Modules.Product.Service;
using e_commerce_server.Src.Core.Common.Enum;
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
        public List<ProductData> GetProducts()
        {
            try
            {
                return _context.Products.ToList();
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
        public List<object> GetProductsByPage(List<ProductData> products, int page)
        {
            try
            {
                return products
                    .Skip((page -1) * 10)
                    .Take(PageSizeEnum.PAGE_SIZE)
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
                        product_status = p.product_status.status,
                        thumbnails = _context.Thumbnails.Where(cond => cond.product_id == p.id).Select(data => data.thumbnail_url).ToList(),
                        p.user_id,
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
        public ProductData UpdateProduct(int productId, AddProductDto productDto)
        {
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

                foreach (var thumbnail in productDto.thumbnails) {
                    ThumbnailData item = new ThumbnailData
                    {
                        thumbnail_url = thumbnail,
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

                return product;
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public ProductData AddProduct(AddProductDto productDto,int userId)
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
                    status_id = productDto.status_id,
                    user_id = userId,
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
        public ProductDto? GetProductById(int id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.id == id);

                var user = userRepository.GetById(product.user_id);

                if (product == null) 
                {
                    return null;
                }

                return new ProductDto
                {
                    id = product.id,
                    name = product.name,
                    price = product.price,
                    discount = product.discount,
                    description = product.description,
                    created_at = product.created_at,
                    updated_at = product.updated_at,
                    status_id = product.status_id,
                    thumbnails = _context.Thumbnails.Where(cond => cond.product_id == product.id).Select(data => data.thumbnail_url).ToList(),
                    user_id = product.user_id,
                    category_id = product.category_id,
                    address = _context.Districts
                        .Where(a => a.id == product.user.district_id)
                        .Select(ct => ct.city.name).SingleOrDefault()
                };
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
    }
}