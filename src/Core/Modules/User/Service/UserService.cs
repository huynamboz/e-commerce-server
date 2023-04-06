using e_commerce_server.Src.Core.Database.Data;

namespace e_commerce_server.Src.Core.Modules.User.Service
{
    public class UserService
    {
        public bool CheckUserStatus(UserData user)
        {
            return !String.IsNullOrEmpty(user.email) &&
                !String.IsNullOrEmpty(user.password) &&
                !String.IsNullOrEmpty(user.name) &&
                !String.IsNullOrEmpty(user.phone_number) &&
                !String.IsNullOrEmpty(user.address) &&
                user.gender != null &&
                user.birthday != null &&
                !String.IsNullOrEmpty(user.avatar) &&
                Convert.ToBoolean(user.district_id);
        }
    }
}
