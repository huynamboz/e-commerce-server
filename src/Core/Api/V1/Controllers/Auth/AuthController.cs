using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using e_commerce_server.src.Packages.HttpException;
using e_commerce_server.src.Core.Modules.Auth.Dto;
using e_commerce_server.src.Core.Modules.Auth.Service;
using e_commerce_server.src.Core.Database.Data;

namespace e_commerce_server.src.Core.Api.V1.Controllers.Auth
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private MyDbContext _context;
        private AuthService authService;

        public AuthController(MyDbContext context)
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
    }
}
