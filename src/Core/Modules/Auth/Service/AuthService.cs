using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Modules.Auth.Dto;
using e_commerce_server.src.Core.Modules.User;
using e_commerce_server.src.Core.Utils;
using e_commerce_server.src.Packages.HttpException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.Serialization;

namespace e_commerce_server.src.Core.Modules.Auth.Service
{
    public class AuthService
    {
        public BCryptService bCryptService;
        private UserRepository userRepository;
        private JwtService jwtService;
        public AuthService(MyDbContext context)
        {
            bCryptService = new BCryptService();
            jwtService = new JwtService();
            userRepository = new UserRepository(context);
        }
        public object Login(LoginModel model)
        {
            var user = userRepository.FindByEmail(model.email);

            if (user == null || user.password != model.password)
            {
                throw new BadRequestException(AuthEnum.LOGIN_INCORRECT);
            }

            return new
            {
                message = AuthEnum.LOGIN_SUCCESS,
                accessToken = jwtService.Sign(user)
            };
        }
    }

}
