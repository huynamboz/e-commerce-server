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

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.password, user.password))
            {
                throw new BadRequestException(AuthEnum.LOGIN_INCORRECT);
            }

            //if (user == null || user.password != model.password)
            //{
            //    throw new BadRequestException(AuthEnum.LOGIN_INCORRECT); 
            //}

            return new
            {
                message = AuthEnum.LOGIN_SUCCESS,
                accessToken = jwtService.Sign(user)
            };
        }

        public static string HashPassword(string password)
        {
            string salt = Environment.GetEnvironmentVariable("PASSWORD_SALT");

            if (salt == null)
            {
                salt = "default_salt_value";
            }
            byte[] saltedPassword = new byte[password.Length + salt.Length];
            Array.Copy(Encoding.UTF8.GetBytes(password), saltedPassword, password.Length);
            Array.Copy(Encoding.UTF8.GetBytes(salt), 0, saltedPassword, password.Length, salt.Length);

            HashAlgorithm algorithm = new SHA256Managed(); 
            byte[] hashedPassword = algorithm.ComputeHash(saltedPassword);

            return Convert.ToBase64String(hashedPassword);

        }

        public object Register(RegisterModel model)
        {
            var existingUser = userRepository.FindByEmail(model.email); 

            if (existingUser != null)
            {
                throw new DuplicateException(AuthEnum.REGISTER_INCORRECT);
            }

            string hashedPassword = HashPassword(model.password);
            //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.password); 

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
