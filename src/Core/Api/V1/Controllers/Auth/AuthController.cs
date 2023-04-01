using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using e_commerce_server.Src.Packages.HttpException;
using e_commerce_server.Src.Core.Modules.Auth.Dto;
using e_commerce_server.Src.Core.Modules.Auth.Service;
using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Core.Modules.User;
using Microsoft.EntityFrameworkCore.Design;

namespace e_commerce_server.Src.Core.Api.V1.Controllers.Auth
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {

        private MyDbContext _context;
        private AuthService authService;

        public authController(MyDbContext context)
        {
            authService = new AuthService(context);
            _context = context;
        }
        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                return Ok(authService.Login(model));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                authService.Register(model);
                return Ok(authService.Register(model));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
    }
}
