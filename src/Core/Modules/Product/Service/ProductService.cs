using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Modules.Media.Service;
using e_commerce_server.src.Core.Modules.Product.Dto;
using e_commerce_server.src.Core.Common.Enum;
using e_commerce_server.src.Core.Modules.User;
using e_commerce_server.src.Core.Modules.User.Service;
using e_commerce_server.src.Packages.HttpExceptions;

namespace e_commerce_server.src.Core.Modules.Product.Service
{
    public class ProductService
    {
        private UserRepository userRepository;
        private ProductRepository productRepository;
        private UserService userService;
        private FileSystemService fileSystemService;
        public ProductService(MyDbContext context) {
            productRepository = new ProductRepository(context);
            userRepository = new UserRepository(context);
            userService = new UserService(context);
            fileSystemService = new FileSystemService();
        }

        public object GetProductsByUserId(int page, int userId)
        {
            var products = productRepository.GetProductsByUserId(userId);
            
            var paginatedProducts = productRepository.GetProductsByUserIdByPage(page, userId);
            
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
        public object GetProductByUserId(int userId, int productId) {
            var product = productRepository.GetProductByIdAndUserId(userId, productId);

            if (product == null)
            {
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
            }

            return new
            {
                data = product
            };
        }
        public object EditProductById(List<string> filePaths, AddProductDto productDto, int productId, int userId)
        {
            try
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

                var user = userRepository.GetUserById(userId);

                if (user == null) {
                    throw new BadRequestException(UserEnum.USER_NOT_FOUND);
                }

                if (userService.CheckUserStatus(user))
                {
                    return new
                    {
                        message = ProductEnum.UPDATE_PRODUCT_SUCCESS,
                        data = productRepository.GetProductById(productRepository.UpdateProduct(filePaths, productId, productDto).id)
                    };
                }

                throw new BadRequestException(ProductEnum.INSUFFICIENT_CONDITION);
            } catch (Exception ex)
            {
                fileSystemService.DeleteFiles(filePaths);
                throw;
            }
        }
        public object DeleteProductById(int userId, int productId)
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

            var user = userRepository.GetUserById(userId);

            if (user == null) {
                throw new BadRequestException(UserEnum.USER_NOT_FOUND);
            }

            if (userService.CheckUserStatus(user))
            {
                productRepository.DeleteProductById(productId);

                return new
                {
                    message = ProductEnum.DELETE_PRODUCT_SUCCESS
                };
            }

            throw new BadRequestException(ProductEnum.INSUFFICIENT_CONDITION);
        }
        public object AddProduct(List<string> filePaths, AddProductDto productDto, int userId)
        {
            try
            {
                var user = userRepository.GetUserById(userId);

                if (user == null) {
                    throw new BadRequestException(UserEnum.USER_NOT_FOUND);
                }

                if (userService.CheckUserStatus(user))
                {
                    return  new
                    {
                        message = ProductEnum.ADD_PRODUCT_SUCCESS,
                        data = productRepository.GetProductById(productRepository.AddProduct(filePaths, productDto, userId).id)
                    };
                }

                throw new BadRequestException(ProductEnum.INSUFFICIENT_CONDITION);
            } catch (Exception ex)
            {
                fileSystemService.DeleteFiles(filePaths);
                throw;
            }
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
