using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Modules.Auth.Dto;
using e_commerce_server.src.Core.Modules.User;
using e_commerce_server.src.Packages.HttpExceptions;
using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Utils;

namespace e_commerce_server.src.Core.Modules.Auth.Service
{
    public class AuthService
    {
        private readonly BCryptService bCryptService;
        private readonly UserRepository userRepository;
        private readonly JwtService jwtService;
        private readonly SendGridService sendGridService;
        public AuthService(AppDbContext context)
        {
            bCryptService = new BCryptService();
            jwtService = new JwtService();
            userRepository = new UserRepository(context);
            sendGridService = new SendGridService();
        }

        public object Login(LoginDto model)
        {
            var user = userRepository.GetUserByEmail(model.email);

            if (user == null || !bCryptService.Verify(model.password, user.password))
            {
                throw new BadRequestException(AuthEnum.LOGIN_INCORRECT);
            }

            if (user.delete_at != null)
            {
                throw new ForbiddenException(AuthEnum.USER_BANNED);
            }

            var accessToken = jwtService.GenerateAccessToken(user);

            var refreshToken = jwtService.GenerateRefreshToken(user);

            user.refresh_token = refreshToken;

            userRepository.CreateOrUpdateUser(user);

            return new
            {
                message = AuthEnum.LOGIN_SUCCESS,
                accessToken,
                refreshToken,
            };
        }

        public object Register(RegisterDto model)
        {
            if (model.password != model.confirm_password)
            {
                throw new BadRequestException(AuthEnum.CONFIRM_PASSWORDS_NOT_MATCH);
            }

            var existingUser = userRepository.GetUserByEmail(model.email);

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
                role_id = 2,
                created_at = DateTime.Now,
                update_at = DateTime.Now
            };

            userRepository.CreateOrUpdateUser(user);

            return new
            {
                message = AuthEnum.REGISTER_SUCCESS,
            };
        }

        public object GenerateAccessToken(RefreshTokenDto model)
        {
            var user = userRepository.GetUserByRefreshToken(model.refresh_token);

            if (user != null)
            {
                var TokenPayload = jwtService.Verify(model.refresh_token);

                if (TokenPayload != null)
                {
                    string? id = TokenPayload.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

                    if (id == user.id.ToString())
                    {
                        var accessToken = jwtService.GenerateAccessToken(user);

                        var refreshToken = jwtService.GenerateRefreshToken(user);

                        user.refresh_token = refreshToken;

                        userRepository.CreateOrUpdateUser(user);

                        return new
                        {
                            accessToken,
                            refreshToken,
                        };
                    }
                }
            }
            throw new UnAuthorizedException(AuthEnum.INVALID_REFRESH_TOKEN);
        }
        public async Task<object> RequestResetPassword(ForgotPasswordDto model)
        {
            var user = Optional.Of(userRepository.GetUserByEmail(model.email)).ThrowIfNotPresent(new BadRequestException(AuthEnum.NOT_FOUND_EMAIL)).Get();

            if (user.delete_at != null)
            {
                throw new ForbiddenException(AuthEnum.USER_BANNED);
            }

            string token = CryptoService.GetRandomString();

            user.reset_token = token;
            user.reset_token_expiration_date = DateTime.Now.AddHours(1);

            userRepository.CreateOrUpdateUser(user);

            try
            {
                await sendGridService.SendMail(model.email, MailContent.REQUEST_RESET_PASSWORD(token));

                return new
                {
                    message = AuthEnum.REQUEST_RESET_PASSWORD_SUCCESS
                };
            } catch(Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public object ResetPassword(ResetPasswordDto model)
        {
            if (model.password != model.confirm_password)
            {
                throw new BadRequestException(AuthEnum.CONFIRM_PASSWORDS_NOT_MATCH);
            }

            var user = Optional.Of(userRepository.GetUserByResetToken(model.reset_token)).ThrowIfNotPresent(new BadRequestException(AuthEnum.INVALID_TOKEN)).Get();

            if (user.reset_token_expiration_date > DateTime.Now)
            {
                user.password = bCryptService.Hash(model.password);
                user.reset_token = null;
                user.reset_token_expiration_date = null;
                user.refresh_token = null;
                user.update_at = DateTime.Now;

                userRepository.CreateOrUpdateUser(user);

                return new
                {
                    message = AuthEnum.UPDATE_PASSWORD_SUCCESS
                };
            }
            throw new BadRequestException(AuthEnum.EXPIRED_TOKEN);
        }
    }
}
