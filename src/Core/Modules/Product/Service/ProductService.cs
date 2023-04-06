using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Modules.Product.Dto;
using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Core.Modules.Auth.Service;
using e_commerce_server.Src.Core.Modules.User;
using e_commerce_server.Src.Packages.HttpException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_server.src.Core.Modules.Product.Service
{
    public class ProductService
    {
        readonly MyDbContext _dbContext;
        private UserRepository userRepository;
        ProductRespository ProductRespository;
        public ProductService(MyDbContext context) {
            _dbContext= context;
            ProductRespository = new ProductRespository(_dbContext);
            userRepository = new UserRepository(_dbContext);
        }
        public object editProductByID(ProductDto productDto, int idProduct, int userID)
        {
            var product = ProductRespository.GetProductByProductId(idProduct);
            if (product == null)
            {
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
            } 
            if (product.user_id != userID )
            {
                throw new BadRequestException(ProductEnum.NOT_HAVE_PERMISSION);
            }  else
            {
                return new
                {
                    success = true,
                    data = ProductRespository.GetProductByProductId(ProductRespository.UpdateProduct(idProduct, productDto).id)
                };
            }
        }
        public object addNewProduct(ProductDto productDto, int idUser)
        {
                if (userRepository.FindById(idUser).active_status)
                {
                    return  new
                    {
                        success = true,
                        data = ProductRespository.GetProductByProductId(ProductRespository.AddNewProduct(productDto, idUser).id)
                    };
                } 
                    throw new BadRequestException(ProductEnum.INSUFFICIENT_CONDITION);
        }
        public object GetAllProduct(int page)
        {
            int pageSize = 20;
            var listProductDbSet = ProductRespository.GetProducts();
            var listProducts = 
                ProductRespository.GetProductByPage(listProductDbSet, page, pageSize);
            int total = (int)Math.Ceiling((double)listProductDbSet.Count() / pageSize); //caculate total pages
            return new
            {
                success = true,
                data = listProducts ,
                meta = new
                {
                    total_pages = total,
                    total_count = listProductDbSet.Count(),
                    current_page = page
                }
            };
        }
        public object GetProductById(int id)
        {
            var product = ProductRespository.GetProductByProductId(id);
            if(product == null)
            {
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
            }
            return new
            {
                success = true,
                data = product
            };
        }
    }
}
