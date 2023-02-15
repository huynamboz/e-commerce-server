using e_commerce_server.src.Packages.HttpException;
using e_commerce_server.src.Core.Database.Data;

namespace e_commerce_server.src.Core.Modules.User
{
    public class UserRepository
    {
        private readonly MyDbContext _context;
        public UserRepository(MyDbContext context) 
        {
            _context = context;
        }
        public UserData FindByEmail(string email)
        {
            try
            {
                return _context.Users.SingleOrDefault(p => p.email == email);
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

    }
}
