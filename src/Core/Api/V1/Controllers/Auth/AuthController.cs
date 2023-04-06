using Microsoft.AspNetCore.Mvc;
using e_commerce_server.Src.Packages.HttpException;
using e_commerce_server.Src.Core.Modules.Auth.Dto;
using e_commerce_server.Src.Core.Modules.Auth.Service;
using e_commerce_server.Src.Core.Database.Data;

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
        public IActionResult Login(LoginDto model)
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
        public IActionResult Register(RegisterDto model)
        {
            try
            { 
                return Ok(authService.Register(model));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken(RefreshTokenDto model)
        {
            try
            {
                return Ok(authService.GenerateRefreshToken(model));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
    }
}
