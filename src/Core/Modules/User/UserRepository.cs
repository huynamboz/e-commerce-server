using e_commerce_server.src.Packages.HttpExceptions;
using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Modules.User.Service;
using e_commerce_server.src.Core.Database;
using Microsoft.EntityFrameworkCore;

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
                    .Include(u => u.products).ThenInclude(p => p.thumbnails)
                    .Include(p => p.products).ThenInclude(p => p.product_status)
                    .Include(p => p.products).ThenInclude(p => p.category)
                    .Include(u => u.favorites)
                    .SingleOrDefault(p => p.id == id);
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public void CreateUser(UserData user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public UserData? GetUserByRefreshToken(string refreshToken)
        {
            try
            {
                return _context.Users.SingleOrDefault(p => p.refresh_token == refreshToken);
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
        public UserData? UpdateUser(UserData user)
        {
            try
            {
                var existingUser = _context.Users.SingleOrDefault(p => p.id == user.id);
        
                if (existingUser != null)
                {
                    existingUser.name = user.name;
                    existingUser.email = user.email;
                    existingUser.password = user.password;
                    existingUser.phone_number = user.phone_number;
                    existingUser.address = user.address;
                    existingUser.gender = user.gender;
                    existingUser.birthday = user.birthday;
                    existingUser.avatar = user.avatar; 
                    existingUser.district_id = user.district_id;
                    existingUser.refresh_token = user.refresh_token;
                    existingUser.reset_token = user.reset_token;
                    existingUser.reset_token_expiration_date = user.reset_token_expiration_date;

                    _context.SaveChanges();

                    return existingUser;
                }
                throw new BadRequestException(UserEnum.USER_NOT_FOUND);
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
        public FavoriteData GetFavoriteByUserIdAndProductId(int userId, int productId)
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
