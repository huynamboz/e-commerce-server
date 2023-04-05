using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Core.Modules.Auth.Dto;
using e_commerce_server.Src.Core.Modules.User;
using e_commerce_server.Src.Core.Utils;
using e_commerce_server.src.Packages.HttpExceptions;
using e_commerce_server.Src.Packages.HttpException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.Serialization;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using e_commerce_server.Src.Core.Env;

namespace e_commerce_server.Src.Core.Modules.Auth.Service
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

            if (user == null || !bCryptService.Verify(model.password, user.password))
            {
                throw new BadRequestException(AuthEnum.LOGIN_INCORRECT);
            }

            return new
            {
                message = AuthEnum.LOGIN_SUCCESS,
                accessToken = jwtService.Sign(user)
            };
        }

        public object Register(RegisterModel model)
        {
            var existingUser = userRepository.FindByEmail(model.email);

            if (existingUser != null)
            {
                throw new DuplicateException(AuthEnum.REGISTER_INCORRECT);
            }

            string hashedPassword = bCryptService.Hash(model.password);

            var user = new UserData
            {
                email = model.email,
                password = hashedPassword,
                name = model.name,
            };

            userRepository.Create(user);

            return new
            {
                message = AuthEnum.REGISTER_SUCCESS,
            };
        }
    }
}
