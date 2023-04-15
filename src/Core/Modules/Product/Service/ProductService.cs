using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Modules.Product.Dto;
using e_commerce_server.src.Core.Common.Enum;
using e_commerce_server.src.Core.Modules.User;
using e_commerce_server.src.Core.Modules.User.Service;
using e_commerce_server.src.Packages.HttpExceptions;
using e_commerce_server.src.Core.Database.Data;

namespace e_commerce_server.src.Core.Modules.Product.Service
{
    public class ProductService
    {
        private readonly UserRepository userRepository;
        private readonly ProductRepository productRepository;
        private readonly UserService userService;
        public ProductService(MyDbContext context)
        {
            productRepository = new ProductRepository(context);
            userRepository = new UserRepository(context);
            userService = new UserService(context);
        }

        public object GetProductsByUserId(int page, int userId)
        {
            var user = userRepository.GetUserById(userId);

            if (user == null)
            {
                throw new BadRequestException(UserEnum.USER_NOT_FOUND);
            }

            if (!user.active_status)
            {
                throw new BadRequestException(ProductEnum.INSUFFICIENT_CONDITION);
            }

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
        public object GetProductByUserId(int userId, int productId)
        {
            var user = userRepository.GetUserById(userId);

            if (user == null)
            {
                throw new BadRequestException(UserEnum.USER_NOT_FOUND);
            }

            var product = productRepository.GetProductByIdAndUserId(userId, productId);

            if (product == null)
            {
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
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
                    product.user.avatar,
                    product.user.active_status,
                    location = Convert.ToBoolean(product.user.district_id) ? $"{product.user.district.name}, {product.user.district.city.name}" : null
                },
                thumbnails = product.thumbnails.Select(t => t.thumbnail_url),
                category = product.category.name,
            };
        }
        public object EditProductById(AddProductDto productDto, int productId, int userId)
        {
            var user = userRepository.GetUserById(userId);

            if (user == null)
            {
                throw new BadRequestException(UserEnum.USER_NOT_FOUND);
            }

            if (!user.active_status)
            {
                throw new BadRequestException(ProductEnum.INSUFFICIENT_CONDITION);
            }

            var product = productRepository.GetProductById(productId);

            if (product == null)
            {
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
            }

            if (product.user.id != userId)
            {
                throw new ForbiddenException(ProductEnum.NOT_HAVE_PERMISSION);
            }

            product.name = productDto.name;
            product.description = productDto.description;
            product.price = productDto.price;
            product.status_id = productDto.status_id;
            product.category_id = productDto.category_id;
            product.updated_at = DateTime.Now;

            productRepository.AddOrUpdateProduct(product, productDto.thumbnailUrls);

            return new
            {
                message = ProductEnum.UPDATE_PRODUCT_SUCCESS,
                data = new
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
            };
        }
        public object DeleteProductById(int userId, int productId)
        {
            var user = userRepository.GetUserById(userId);

            if (user == null)
            {
                throw new BadRequestException(UserEnum.USER_NOT_FOUND);
            }

            if (!user.active_status)
            {
                throw new BadRequestException(ProductEnum.INSUFFICIENT_CONDITION);
            }

            var product = productRepository.GetProductById(productId);

            if (product == null)
            {
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
            }

            if (product.user.id != userId)
            {
               throw new ForbiddenException(ProductEnum.NOT_HAVE_PERMISSION);
            }

            productRepository.DeleteProductById(productId);

            return new
            {
                message = ProductEnum.DELETE_PRODUCT_SUCCESS
            };
        }
        public object AddProduct(AddProductDto productDto, int userId)
        {
            var user = userRepository.GetUserById(userId);

            if (user == null)
            {
                throw new BadRequestException(UserEnum.USER_NOT_FOUND);
            }

            if (!user.active_status)
            {
                throw new BadRequestException(ProductEnum.INSUFFICIENT_CONDITION);
            }

            ProductData product = new ProductData 
            {
                name = productDto.name,
                description = productDto.description,
                price = productDto.price,
                user_id = userId,
                status_id = productDto.status_id,
                category_id = productDto.category_id,
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            };

            var newProduct = productRepository.GetProductById(productRepository.AddOrUpdateProduct(product, productDto.thumbnailUrls).id);

            return new
            {
                message = ProductEnum.ADD_PRODUCT_SUCCESS,
                data = new 
                {
                    newProduct.id,
                    newProduct.name,
                    newProduct.price,
                    newProduct.discount,
                    newProduct.description,
                    newProduct.created_at,
                    newProduct.updated_at,
                    newProduct.product_status.status,
                    user = new
                    {
                        newProduct.user.id,
                        newProduct.user.name,
                        newProduct.user.phone_number,
                        newProduct.user.avatar,
                        location = Convert.ToBoolean(newProduct.user.district_id) ? $"{newProduct.user.district.name}, {newProduct.user.district.city.name}" : null
                    },
                    thumbnails = newProduct.thumbnails.Select(t => t.thumbnail_url),
                    category = newProduct.category.name,
                    
                }
            };
        }
        public object GetAllProducts(int page)
        {
            var products = productRepository.GetProducts();

            var paginatedProducts = productRepository.GetProductsByPage(page);

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
            };
        }
    }
}
