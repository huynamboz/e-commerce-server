using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Packages.HttpExceptions;
using e_commerce_server.src.Core.Modules.User.Dto;
using e_commerce_server.src.Core.Modules.Auth.Service;
using e_commerce_server.src.Core.Modules.Media.Service;
using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Modules.Product.Service;
using e_commerce_server.src.Core.Modules.Product;
using e_commerce_server.src.Core.Common.Enum;

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
    
        public bool CheckUserStatus(dynamic user)
        {
            return !String.IsNullOrEmpty(user.email) &&
                !String.IsNullOrEmpty(user.name) &&
                !String.IsNullOrEmpty(user.phone_number) &&
                !String.IsNullOrEmpty(user.address) &&
                user.gender != null &&
                user.birthday != null &&
                !String.IsNullOrEmpty(user.avatar) &&
                Convert.ToBoolean(user.district);
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

                UserData updatedUser = new UserData();

                updatedUser.id = userId;
                updatedUser.name = updateUserDto.name;
                updatedUser.email = updateUserDto.email;
                updatedUser.phone_number = updateUserDto.phone_number;
                updatedUser.password = bCryptService.Hash(updateUserDto.password);
                updatedUser.gender = updateUserDto.gender;
                updatedUser.birthday = updateUserDto.birthday;
                updatedUser.address = updateUserDto.address;
                updatedUser.avatar = string.IsNullOrEmpty(filePath) ? null : mediaService.UploadOne(filePath, $"BadSupermarket/users/{userId}/avatar/");
                updatedUser.district_id = updateUserDto.district_id;
                updatedUser.update_at = DateTime.Now;

                return new
                {
                    message = UserEnum.UPDATE_USER_SUCCESS,
                    data = userRepository.CreateOrUpdateUser(updatedUser)
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
        public object GetFavoriteProducts(int page, int userId)
        {
            var user = userRepository.GetUserById(userId);

            if (user == null)
            {
                throw new BadRequestException(UserEnum.USER_NOT_FOUND);
            }

            var products = userRepository.GetFavoriteProductsByUserId(userId);

            var paginatedProducts = userRepository.GetFavoriteProductsByUserIdByPage(page, userId);

            int total = (int)Math.Ceiling((double)products.Count() / PageSizeEnum.PAGE_SIZE); //calculate total pages

            return new
            {
                data = paginatedProducts,
                meta = new
                {
                    totalPages = total,
                    totalCount = products.Count(),
                    currentPage = page
                }
            };
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

            var existingFavoriteProduct = userRepository.GetFavoriteProductByUserIdAndProductId(userId, model.product_id);

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

            var existingFavoriteProduct = userRepository.GetFavoriteProductByUserIdAndProductId(userId, productId);

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
        public object GetAllUsers(int roleId) 
        {
            if(roleId == 2)
            {
                return userRepository.GetAllUsers();
            }
            throw new BadRequestException();
        }
    }
}
