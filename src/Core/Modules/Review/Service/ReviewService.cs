using e_commerce_server.src.Core.Modules.User.Service;
using e_commerce_server.src.Core.Modules.User;
using e_commerce_server.src.Packages.HttpExceptions;
using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Modules.Product.Service;
using e_commerce_server.src.Core.Modules.Product;
using e_commerce_server.src.Core.Modules.Review.Dto;
using e_commerce_server.src.Core.Database.Data;

namespace e_commerce_server.src.Core.Modules.Review.Service
{
    public class ReviewService
    {
        private readonly ProductRepository productRepository;
        private readonly UserRepository userRepository;
        private readonly ReviewRepository reviewRepository;
        public ReviewService(MyDbContext context)
        {
            productRepository = new ProductRepository(context);
            userRepository = new UserRepository(context);
            reviewRepository = new ReviewRepository(context);
        }
        public object ReviewProduct(int productId, int userId ,ReviewProductDto model)
        {
            var product = productRepository.GetProductById(productId);

            if (product == null) 
            { 
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
            }

            var user = userRepository.GetUserById(userId);

            if (user == null)
            {
                throw new BadRequestException(UserEnum.USER_NOT_FOUND);
            }

            var review = new ReviewData
            {
                comment = model.comment,
                rating = model.rating,
                product_id = productId,
                user_id = userId,
                create_at = DateTime.Now,
                update_at = DateTime.Now,
            };

            reviewRepository.CreateReview(review);

            return new
            {
                message = UserEnum.ADD_TO_FAVORITE_SUCCESS
            };
        }
    }
}
