using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Modules.Product.Dto;
using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Core.Modules.Auth.Service;
using e_commerce_server.Src.Packages.HttpException;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_server.src.Core.Modules.Product.Service
{
    public class ProductService
    {
        readonly MyDbContext _dbContext;
        public ProductService(MyDbContext context) {
            _dbContext= context;
        }
        public object editProductByID(ProductDto productDto, int idProduct)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.id== idProduct);
            if (product == null)
            {
                throw new BadRequestException("Product not exist");
            } else
            {
                product.description = productDto.description;
                product.price = productDto.price;
                product.name = productDto.name;
                product.updated_at = DateTime.Now;
                product.category_id = productDto.category_id;
                product.discount = productDto.discount;
                product.thumbnail_url = productDto.thumbnail_url;
                _dbContext.SaveChanges();
                return product;
            }
        }
        public object addNewProduct(ProductDto productDto, int idUser)
        {
            try
            {
                var newproduct = new ProductData
                {
                    name = productDto.name,
                    description = productDto.description,
                    price = productDto.price,
                    discount = productDto.discount,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now,
                    thumbnail_url = productDto.thumbnail_url,
                    product_status = productDto.product_status,
                    user_id = idUser,
                    category_id = productDto.category_id,
                };
                _dbContext.Add(newproduct);
                _dbContext.SaveChanges();
                return productDto;
            } catch (Exception ex)
            {
                return new BadRequestException(ex.ToString());
            }
        }
        public object GetAllProduct()
        {
            try
            {
                var listProduct = _dbContext.Products.Select(
                p => new
                {
                    p.id,
                    p.name,
                    p.price,
                    p.discount,
                    p.description,
                    p.created_at,
                    p.updated_at,
                    p.thumbnail_url,
                    p.active_status,
                    p.product_status,
                    p.user_id,
                    p.category_id,
                    category = p.category.name,
                    address = _dbContext.Districts
                        .Where(a => a.id == p.user.district_id)
                        .Select(ct => ct.city.name).SingleOrDefault()
                }
            ).ToList();

                return new
                {
                    success = true,
                    data = listProduct
                };
            } catch (Exception ex)
            {
                return ex;
            }
        }
        
    }
}
