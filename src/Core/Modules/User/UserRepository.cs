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
        public UserData? FindByEmail(string email)
        {
            try
            {
                return _context.Users.SingleOrDefault(p => p.email == email);
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

        public void Create(UserData user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

        public UserData? FindByRefreshToken(string refreshToken)
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
                    existingUser.refresh_token = user.refresh_token;
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
