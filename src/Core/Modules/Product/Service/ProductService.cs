using e_commerce_server.src.Core.Modules.Product.Dto;
using e_commerce_server.Src.Core.Common.Enum;
using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Core.Modules.User;
using e_commerce_server.Src.Packages.HttpException;

namespace e_commerce_server.src.Core.Modules.Product.Service
{
    public class ProductService
    {
        readonly MyDbContext _dbContext;
        private UserRepository userRepository;
        ProductRepository productRepository;
        public ProductService(MyDbContext context) {
            _dbContext= context;
            productRepository = new ProductRepository(_dbContext);
            userRepository = new UserRepository(_dbContext);
        }
        public object editProductById(ProductDto productDto, int productId, int userId)
        {
            var product = productRepository.GetProductById(productId);

            if (product == null)
            {
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
            }

            if (product.user_id != userId)
            {
                throw new BadRequestException(ProductEnum.NOT_HAVE_PERMISSION);
            }  else
            {
                return new
                {
                    data = productRepository.GetProductById(productRepository.UpdateProduct(productId, productDto).id)
                };
            }
        }
        public object addProduct(ProductDto productDto, int userId)
        {
                //if (userRepository.FindById(userId).active_status)
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
            var listProductDbSet = productRepository.GetProducts();

            var listProducts = productRepository.GetProductByPage(listProductDbSet, page, PageSizeEnum.PAGE_SIZE);

            int total = (int)Math.Ceiling((double)listProductDbSet.Count() / PageSizeEnum.PAGE_SIZE); //calculate total pages

            return new
            {
                data = listProducts,
                meta = new
                {
                    totalPages = total,
                    totalCount = listProductDbSet.Count(),
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
