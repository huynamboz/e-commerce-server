using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Packages.HttpExceptions;
using Microsoft.EntityFrameworkCore;

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
        
        public List<object> GetReviewsByUserId (int userId)
        {
            try
            {
                var reviews = _context.Reviews
                    .Where(r => r.product.user_id == userId)
                    .Include(r => r.product).ThenInclude(p => p.thumbnails)
                    .Include(r => r.user)
                    .Select(r => new
                    {
                        product = new
                        {
                            r.product.id,
                            r.product.name,
                            thumbnails = r.product.thumbnails.Select(t => t.thumbnail_url)
                        },
                        user = new
                        {
                            r.user.id,
                            r.user.name,
                            r.user.avatar
                        },
                        r.rating,
                        r.comment,
                        r.create_at,
                        r.update_at
                    }).Cast<object>().ToList(); 
                return reviews;
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

        public void DeleteReview(int productId, int userId)
        { 

            var review = _context.Reviews.Find(productId, userId);

            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }
        }
    }
}
