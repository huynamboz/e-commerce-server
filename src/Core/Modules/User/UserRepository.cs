using e_commerce_server.Src.Packages.HttpException;
using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Core.Modules.User.Service;

namespace e_commerce_server.Src.Core.Modules.User
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
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public UserData? GetUserById(int id)
        {
            try
            {
                return _context.Users.SingleOrDefault(p => p.id == id);
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public void CreateUser(UserData user)
        {
            try
            {
                user.created_at = DateTime.Now;

                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

        public UserData? GetUserByRefreshToken(string refreshToken)
        {
            try
            {
                return _context.Users.SingleOrDefault(p => p.refresh_token == refreshToken);
            }
            catch (Exception ex)
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

                    _context.SaveChanges();

                    return existingUser;
                }
                throw new BadRequestException(UserEnum.USER_NOT_FOUND);
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
    }
}
