using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Packages.HttpExceptions;
using e_commerce_server.src.Core.Modules.User.Dto;
using e_commerce_server.src.Core.Modules.Auth.Service;
using e_commerce_server.src.Core.Modules.Media.Service;
using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Modules.Product.Service;
using e_commerce_server.src.Core.Modules.Product;

namespace e_commerce_server.src.Core.Modules.User.Service
{
    public class UserService
    {
        private ProductRepository productRepository;
        private FileSystemService fileSystemService;
        private UserRepository userRepository;
        private BCryptService bCryptService;
        private MediaService mediaService;
        public UserService(MyDbContext context)
        {
            productRepository = new ProductRepository(context);
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
        public object AddProductToFavorite(int userId, AddProductToFavoriteDto model)
        {
            var product = productRepository.GetProductById(model.product_id);

            if (product == null)
            {
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
            }

            var user = userRepository.GetUserById(userId);

            if (user == null)
            {
                throw new BadRequestException(UserEnum.USER_NOT_FOUND);
            }

            var existingFavoriteProduct = userRepository.GetFavoriteByUserIdAndProductId(userId, model.product_id);

            if (existingFavoriteProduct != null)
            {
                throw new BadRequestException(UserEnum.DUPLICATE_FAVORITE_PRODUCT);
            }

            FavoriteData favorite = new()
            {
                user_id = userId,
                product_id = model.product_id,
                create_at = DateTime.Now
            };

            userRepository.AddProductToFavorite(favorite);

            return new
            {
                message = UserEnum.ADD_TO_FAVORITE_SUCCESS
            };
        }
        public object RemoveProductFromFavorite(int userId, int productId)
        {
            var user = userRepository.GetUserById(userId);

            if (user == null)
            {
                throw new BadRequestException(UserEnum.USER_NOT_FOUND);
            }

            var existingFavoriteProduct = userRepository.GetFavoriteByUserIdAndProductId(userId, productId);

            if (existingFavoriteProduct == null)
            {
                throw new BadRequestException(UserEnum.FAVORITE_PRODUCT_NOT_FOUND);
            }

            userRepository.RemoveProductFromFavorite(existingFavoriteProduct);

            return new
            {
                message = UserEnum.REMOVE_FROM_FAVORITE_SUCCESS
            };
        }
    }
}
