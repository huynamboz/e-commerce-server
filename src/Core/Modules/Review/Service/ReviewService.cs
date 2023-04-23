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
        public object CreateOrUpdateReview(int productId, int userId, ReviewProductDto model)
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

            if (product.user_id == userId)
            {
                throw new ForbiddenException(ReviewEnum.CANNOT_REVIEW_OWN_PRODUCT);
            }

            var review = reviewRepository.GetReviewByIds(userId, productId);

            if (review != null)
            {
                review.rating = model.rating;
                review.comment = model.comment;
                review.update_at = DateTime.Now;

                reviewRepository.UpdateReview(review);

                return new
                {
                    message = ReviewEnum.UPDATE_REVIEW_SUCCESS,
                    data = new
                    {
                        review.rating,
                        review.comment,
                        review.update_at
                    }
                };
            }

            var newReview = new ReviewData
            {
                comment = model.comment,
                rating = model.rating,
                product_id = productId,
                user_id = userId,
                create_at = DateTime.Now,
                update_at = DateTime.Now,
            };

            reviewRepository.CreateReview(newReview);

            return new
            {
                message = ReviewEnum.CREATE_REVIEW_SUCCESS,
                data = new
                {
                    newReview.rating,
                    newReview.comment,
                    newReview.update_at
                }
            };
        }
    }
}
