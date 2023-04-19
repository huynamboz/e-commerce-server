using e_commerce_server.src.Packages.HttpExceptions;
using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Database;
using Microsoft.EntityFrameworkCore;
using e_commerce_server.src.Core.Common.Enum;

namespace e_commerce_server.src.Core.Modules.User
{
    public class UserRepository
    {
        private readonly MyDbContext _context;
        public UserRepository(MyDbContext context)
        {
            _context = context;
        }
        public UserData? GetUserByEmail(string email)
        {
            try
            {
                return _context.Users.SingleOrDefault(p => p.email == email);
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public UserData? GetUserByPhoneNumber(string phoneNumber)
        {
            try
            {
                return _context.Users.SingleOrDefault(p => p.phone_number == phoneNumber);
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public UserData? GetUserById(int id)
        {
            try
            {
                return _context.Users
                    .Include(u => u.district).ThenInclude(d => d.city)
                    .SingleOrDefault(p => p.id == id);
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public UserData? GetUserByRefreshToken(string refreshToken)
        {
            try
            {
                return _context.Users.SingleOrDefault(u => u.refresh_token == refreshToken && u.delete_at != null);
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public UserData? GetUserByResetToken(string resetToken)
        {
            try
            {
                return _context.Users.SingleOrDefault(p => p.reset_token == resetToken);

            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public UserData? CreateOrUpdateUser(UserData user)
        {
            try
            {
                if (user.id == 0)
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                } else {
                    _context.SaveChanges();
                    if (user.district_id != null) 
                    {
                        _context.Entry(user).Reference(u => u.district).Load();
                        _context.Entry(user.district).Reference(d => d.city).Load();
                    }
                }

                return user;
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

        public int GetUserIdByProductId(int productId)
        {
            try
            {
                var product = _context.Products
                .Include(p => p.user)
                .SingleOrDefault(p => p.id == productId);
                return product.user.id;

            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        

        public List<object> GetAllUsers()
        {
            try
            {
                return _context.Users
                    .Where(u => u.role_id == RoleEnum.USER)
                    .Include(u => u.district).ThenInclude(d => d.city)
                    .Select(user => new
                    {
                        user.id,
                        user.email,
                        user.name,
                        user.phone_number,
                        user.address,
                        user.gender,
                        user.birthday,
                        user.avatar,
                        user.role_id,
                        user.created_at,
                        user.delete_at,
                        user.active_status,
                        location  = Convert.ToBoolean(user.district_id) ? $"{user.district.name}, {user.district.city.name}" : null
                    }).Cast<object>().ToList();
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public void AddProductToFavorite(FavoriteData favorite)
        {
            try
            {
                _context.Favorites.Add(favorite);
                _context.SaveChanges();
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public List<FavoriteData> GetFavoriteProductsByUserId(int userId)
        {
            try
            {
                return _context.Favorites.Where(p => p.user_id == userId && p.user.active_status).ToList();
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public List<object> GetFavoriteProductsByUserIdByPage(int page, int userId)
        {
            try
            {
                return _context.Favorites
                    .Where(p => p.user_id == userId && p.product.user.active_status)
                    .Skip((page -1) * 10)
                    .Take(PageSizeEnum.PAGE_SIZE)
                    .Include(p => p.product).ThenInclude(p => p.category)
                    .Include(p => p.product).ThenInclude(p => p.product_status)
                    .Select(p => new
                    {
                        p.product.id,
                        p.product.name,
                        p.product.price,
                        p.product.discount,
                        p.product.description,
                        p.product.created_at,
                        p.product.updated_at,
                        p.product.product_status.status,
                        user = new
                        {
                            p.product.user.id,
                            p.product.user.name,
                            p.product.user.phone_number,
                            p.product.user.avatar,
                            location = Convert.ToBoolean(p.user.district_id) ? $"{p.user.district.name}, {p.user.district.city.name}" : null
                        },
                        thumbnails = p.product.thumbnails.Select(t => t.thumbnail_url),
                        category = p.product.category.name,
                    }
                ).Cast<object>().ToList();
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public FavoriteData? GetFavoriteProductByUserIdAndProductId(int userId, int productId)
        {
            try
            {
                return _context.Favorites.SingleOrDefault(p => p.user_id == userId && p.product_id == productId);
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

        public void RemoveProductFromFavorite(FavoriteData favorite)
        {
            try
            {
                _context.Favorites.Remove(favorite);
                _context.SaveChanges();
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
    }
}
