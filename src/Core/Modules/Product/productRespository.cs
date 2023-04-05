using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Modules.Product.Dto;
using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Packages.HttpException;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_server.src.Core.Modules.Product
{
    public class ProductRespository
    {
        private readonly MyDbContext _context;
        public ProductRespository(MyDbContext context)
        {
            _context = context;
        }
        public ProductData GetProductByProductId(int id)
        {
            try
            {
                return _context.Products.FirstOrDefault(p => p.id == id);
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public List<object> GetAllProducts()
        {
            try
            {
                return _context.Products.Select(
                    p => new
                    {
                        p.id,
                        p.name,
                        p.price,
                        p.discount,
                        p.description,
                        p.created_at,
                        p.updated_at,
                        p.active_status,
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
        public ProductData UpdateProduct(ProductData product, ProductDto productDto)
        {
            try
            {
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
        public ProductData AddNewProduct(ProductDto productDto,int idUser)
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
                    product_status = productDto.product_status,
                    user_id = idUser,
                    category_id = productDto.category_id,
                };
                _context.Add(newproduct);
                _context.SaveChanges();
                return newproduct;
            } catch (Exception e)
            {
                throw new InternalException(e.Message);
            }
        }
        public object GetProductByIdSpecial(int id)
        {
            try
            {
                return _context.Products.Where(i => i.id == id).Select(
                p => new
                {
                    p.id,
                    p.name,
                    p.price,
                    p.discount,
                    p.description,
                    p.created_at,
                    p.updated_at,
                    p.active_status,
                    p.product_status,
                    thumbnails = _context.Thumbnails.Where(cond => cond.product_id == p.id).Select(data => data.thumbnail_url).ToList(),
                    p.user_id,
                    p.category_id,
                    category = p.category.name,
                    address = _context.Districts
                        .Where(a => a.id == p.user.district_id)
                        .Select(ct => ct.city.name).SingleOrDefault()
                }
            ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
    }
}
