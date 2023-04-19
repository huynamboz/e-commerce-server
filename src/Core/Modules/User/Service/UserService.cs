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
        private readonly ProductRepository productRepository;
        private readonly FileSystemService fileSystemService;
        private readonly UserRepository userRepository;
        private readonly BCryptService bCryptService;
        private readonly MediaService mediaService;
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
            return !String.IsNullOrEmpty(user.phone_number) &&
                !String.IsNullOrEmpty(user.address) &&
                user.gender != null &&
                user.birthday != null &&
                !String.IsNullOrEmpty(user.avatar) &&
                Convert.ToBoolean(user.district_id) &&
                user.delete_at == null;
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

            if (userById.delete_at != null)
            {
                throw new ForbiddenException(AuthEnum.USER_BANNED);
            }

            userById.name = updateUserDto.name;
            userById.email = updateUserDto.email;
            userById.phone_number = updateUserDto.phone_number;
            userById.password = bCryptService.Hash(updateUserDto.password);
            userById.gender = updateUserDto.gender;
            userById.birthday = updateUserDto.birthday;
            userById.address = updateUserDto.address;
            userById.avatar = updateUserDto.avatar;
            userById.district_id = updateUserDto.district_id;
            userById.update_at = DateTime.Now;
            userById.active_status = CheckUserStatus(userById);

            userRepository.CreateOrUpdateUser(userById);

            return new
            {
                message = UserEnum.UPDATE_USER_SUCCESS,
                data = new
                {
                    userById.id,
                    userById.name,
                    userById.email,
                    userById.phone_number,
                    userById.address,
                    userById.avatar,
                    userById.birthday,
                    userById.created_at,
                    userById.update_at,
                    userById.gender,
                    userById.active_status,
                    location = Convert.ToBoolean(userById?.district_id) ? $"{userById.district?.name}, {userById.district?.city.name}" : null
                }
            };
        }
        public object GetUserById(int userId)
        {
            var user = userRepository.GetUserById(userId) ?? throw new BadRequestException(UserEnum.USER_NOT_FOUND);

            return new {
                user.id,
                user.name,
                user.email,
                user.phone_number,
                user.address,
                user.avatar,
                user.birthday,
                user.created_at,
                user.update_at,
                user.delete_at,
                user.gender,
                user.active_status,
                location = Convert.ToBoolean(user.district_id) ? $"{user.district.name}, {user.district.city.name}" : null
            };
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
            if(roleId == RoleEnum.ADMIN)
            {
                return userRepository.GetAllUsers();
            }
            throw new ForbiddenException(UserEnum.GET_ALL_USERS_DENIED);
        }
        public object DeleteUserById(int roleId, int userId)
        {
            var user = userRepository.GetUserById(userId);

            if(user == null)
            {
                throw new BadRequestException(UserEnum.USER_NOT_FOUND);
            }

            if (roleId != RoleEnum.ADMIN || user.role_id == RoleEnum.ADMIN)
            {
                throw new BadRequestException(UserEnum.DELETE_USER_DENIED);
            }

            if (user.delete_at != null) {
                throw new BadRequestException(UserEnum.USER_ALREADY_DELETED);
            }

            user.delete_at = DateTime.Now;
            user.active_status = false;
            user.refresh_token = null;

            userRepository.CreateOrUpdateUser(user);

            return new
            {
                message = UserEnum.DELETE_USER_SUCCESS
            };
        }

        public int GetUserByProductId(int productId)
        {
            int userId = userRepository.GetUserIdByProductId(productId);

            var user = userRepository.GetUserById(userId) ?? throw new BadRequestException(UserEnum.USER_NOT_FOUND);

            return user.role_id;
        }
    }
}
