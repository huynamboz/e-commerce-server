using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Packages.HttpException;
using e_commerce_server.src.Core.Modules.User.Dto;
using e_commerce_server.Src.Core.Modules.Auth.Service;

namespace e_commerce_server.Src.Core.Modules.User.Service
{
    public class UserService
    {
        private UserRepository userRepository;
        private BCryptService bCryptService;
        public UserService(MyDbContext context)
        {
            userRepository = new UserRepository(context);
            bCryptService = new BCryptService();
        } 
    
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

        public object UpdateUserById(UpdateUserDto updateUserDto, int userId)
        {
            var userByEmail = userRepository.GetUserByEmail(updateUserDto.email);
            
            if (userByEmail != null && userByEmail.id != userId)
            {
                throw new BadRequestException(AuthEnum.DUPLICATE_EMAIL);
            }

            if (!String.IsNullOrEmpty(updateUserDto.phone_number))
            {
                var userByPhone = userRepository.GetUserByPhoneNumber(updateUserDto.phone_number);

                if (userByPhone != null && userByPhone.id != userId)
                {
                    throw new BadRequestException(AuthEnum.DUPLICATE_PHONE_NUMBER);
                }
            }

            var userById = userRepository.GetUserById(userId) ?? throw new BadRequestException(UserEnum.USER_NOT_FOUND);
            userById.name = updateUserDto.name;
            userById.email = updateUserDto.email;
            userById.phone_number = updateUserDto.phone_number;
            userById.password = bCryptService.Hash(updateUserDto.password);
            userById.gender = updateUserDto.gender;
            userById.birthday = updateUserDto.birthday;
            userById.address = updateUserDto.address;
            userById.avatar = updateUserDto.avatar;
            userById.district_id = updateUserDto.district_id;

            return new
            {
                message = UserEnum.UPDATE_USER_SUCCESS,
                data = userRepository.UpdateUser(userById)
            };
        }
    }
}
