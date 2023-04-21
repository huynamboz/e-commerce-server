﻿using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Packages.HttpExceptions;

namespace e_commerce_server.src.Core.Modules.Review
{
    public class ReviewRepository
    {
        private readonly MyDbContext _context;
        public ReviewRepository(MyDbContext context)
        {
            _context = context;
        }
        public void CreateReview(ReviewData review)
        {
            try
            {
                _context.Reviews.Add(review);
                _context.SaveChanges();
            }

            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public void UpdateReview(ReviewData review) {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        } 
        public ReviewData? GetReviewByIds(int userId, int productId)
        {
            try
            {
                return _context.Reviews.SingleOrDefault(r  => r.user_id == userId && r.product_id == productId);
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

        public object GetProductByProductId(int productId)
        {
            var userProductReviews = 
                from r in _context.Reviews
                join p in _context.Products on r.product_id equals p.id 
                join u in _context.Users on p.user_id equals u.id
                select new
                {
                       UserId = u.id,
                       ProductId = p.id,
                       ReviewComment = r.comment,
                       ReviewRating = r.rating
                };

            return userProductReviews;
        }
    }
}
