﻿using e_commerce_server.src.Core.Modules.Product.Dto;
using e_commerce_server.Src.Core.Common.Enum;
using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Core.Modules.User;
using e_commerce_server.Src.Core.Modules.User.Service;
using e_commerce_server.Src.Packages.HttpException;

namespace e_commerce_server.src.Core.Modules.Product.Service
{
    public class ProductService
    {
        private UserRepository userRepository;
        private ProductRepository productRepository;
        private UserService userService;
        public ProductService(MyDbContext context) {
            productRepository = new ProductRepository(context);
            userRepository = new UserRepository(context);
            userService = new UserService();
        }

        public object GetProductsByUserId(int page, int userId)
        {
            var products = productRepository.GetProductsByUserId(userId);
            
            var paginatedProducts = productRepository.GetProductsByPage(products, page);
            
            int total = (int)Math.Ceiling((double)products.Count() / PageSizeEnum.PAGE_SIZE); //calculate total pages

            return new
            {
                data = paginatedProducts,
                meta = new
                {
                    totalPages = total,
                    totalCount = products.Count(),
                    currentPage = page
                }
            };
        }
        public object EditProductById(AddProductDto productDto, int productId, int userId)
        {
            var product = productRepository.GetProductById(productId);

            if (product == null)
            {
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
            }

            if (product.user_id != userId)
            {
                throw new BadRequestException(ProductEnum.NOT_HAVE_PERMISSION);
            }

            var user = userRepository.GetById(userId);

            if (userService.CheckUserStatus(user))
            {
                return new
                {
                    data = productRepository.GetProductById(productRepository.UpdateProduct(productId, productDto).id)
                };
            }

            throw new BadRequestException(ProductEnum.INSUFFICIENT_CONDITION);
        }
        public object AddProduct(AddProductDto productDto, int userId)
        {
            var user = userRepository.GetById(userId);
            
            if (userService.CheckUserStatus(user))
            {
                return  new
                {
                    data = productRepository.GetProductById(productRepository.AddProduct(productDto, userId).id)
                };
            } 
            throw new BadRequestException(ProductEnum.INSUFFICIENT_CONDITION);
        }
        public object GetAllProducts(int page)
        {
            var products = productRepository.GetProducts();

            var paginatedProducts = productRepository.GetProductsByPage(products, page);

            int total = (int)Math.Ceiling((double)products.Count() / PageSizeEnum.PAGE_SIZE); //calculate total pages

            return new
            {
                data = paginatedProducts,
                meta = new
                {
                    totalPages = total,
                    totalCount = products.Count(),
                    currentPage = page
                }
            };
        }
        public object GetProductById(int id)
        {
            var product = productRepository.GetProductById(id);

            if(product == null)
            {
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
            }

            return new
            {
                data = product
            };
        }
    }
}