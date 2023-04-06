using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Core.Modules.Auth.Dto;
using e_commerce_server.Src.Core.Modules.User;
using e_commerce_server.src.Packages.HttpExceptions;
using e_commerce_server.Src.Packages.HttpException;

namespace e_commerce_server.Src.Core.Modules.Auth.Service
{
    public class AuthService
    {
        private BCryptService bCryptService;
        private UserRepository userRepository;
        private JwtService jwtService;
        public AuthService(MyDbContext context)
        {
            bCryptService = new BCryptService();
            jwtService = new JwtService();
            userRepository = new UserRepository(context);
        }

        public object Login(LoginDto model)
        {
            var user = userRepository.FindByEmail(model.email);

            if (user == null || !bCryptService.Verify(model.password, user.password))
            {
                throw new BadRequestException(AuthEnum.LOGIN_INCORRECT);
            }

            var accessToken = jwtService.GenerateAccessToken(user);

            var refreshToken = jwtService.generateRefreshToken(user);

            user.refresh_token = refreshToken;

            userRepository.UpdateUser(user);

            return new
            {
                message = AuthEnum.LOGIN_SUCCESS,
                accessToken,
                refreshToken,
            };
        }

        public object Register(RegisterDto model)
        {
            var existingUser = userRepository.FindByEmail(model.email);

            if (existingUser != null)
            {
                throw new DuplicateException(AuthEnum.DUPLICATE_EMAIL);
            }

            string hashedPassword = bCryptService.Hash(model.password);

            var user = new UserData
            {
                email = model.email,
                password = hashedPassword,
                name = model.name,
                role_id = 1,
            };

            userRepository.Create(user);

            return new
            {
                message = AuthEnum.REGISTER_SUCCESS,
            };
        }

        public object GenerateRefreshToken(RefreshTokenDto model) {
            var user = userRepository.FindByRefreshToken(model.refresh_token);

            if (user != null) {
                var TokenPayload = jwtService.Verify(model.refresh_token);

                if (TokenPayload != null) {
                    string? id = TokenPayload.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

                    if (id == user.id.ToString()) {
                        var accessToken = jwtService.GenerateAccessToken(user);

                        var refreshToken = jwtService.generateRefreshToken(user);

                        user.refresh_token = refreshToken;

                        userRepository.UpdateUser(user);

                        return new
                        {
                            accessToken,
                            refreshToken,
                        };
                    }
                }
            }

            throw new UnAuthorizedException("Refresh token is invalid");
        }
    }
}
