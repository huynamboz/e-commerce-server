using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Packages.HttpException;
using e_commerce_server.src.Core.Modules.User.Dto;
using e_commerce_server.Src.Core.Modules.Auth.Service;
using e_commerce_server.src.Core.Modules.Media.Service;

namespace e_commerce_server.Src.Core.Modules.User.Service
{
    public class UserService
    {
        private FileSystemService fileSystemService;
        private UserRepository userRepository;
        private BCryptService bCryptService;
        private MediaService mediaService;
        public UserService(MyDbContext context)
        {
            userRepository = new UserRepository(context);
            fileSystemService = new FileSystemService();
            bCryptService = new BCryptService();
            mediaService = new MediaService();
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

        public object UpdateUserById(string? filePath, UpdateUserDto updateUserDto, int userId)
        {
            try
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
                userById.avatar = string.IsNullOrEmpty(filePath) ? null : mediaService.UploadOne(filePath, $"BadSupermarket/users/{userId}/avatar/");
                userById.district_id = updateUserDto.district_id;

                return new
                {
                    message = UserEnum.UPDATE_USER_SUCCESS,
                    data = userRepository.UpdateUser(userById)
                };
            } catch(Exception ex)
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    fileSystemService.DeleteFile(filePath);
                }
                throw;
            }
        }

        public object GetUserById(int userId)
        {
            return userRepository.GetUserById(userId) ?? throw new BadRequestException(UserEnum.USER_NOT_FOUND);
        }
    }
}
