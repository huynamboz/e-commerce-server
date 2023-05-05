using Microsoft.AspNetCore.Mvc;
using e_commerce_server.src.Packages.HttpExceptions;
using e_commerce_server.src.Core.Modules.Auth.Dto;
using e_commerce_server.src.Core.Modules.Auth.Service;
using System.Web;
using e_commerce_server.src.Core.Database;

namespace e_commerce_server.src.Core.Api.V1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {

        private readonly AuthService authService;

        public authController(MyDbContext context)
        {
            authService = new AuthService(context);
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
                return Ok(authService.GenerateAccessToken(model));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            try
            {
                return Ok(await authService.RequestResetPassword(model));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword(ResetPasswordDto model)
        {
            try
            {
                return Ok(authService.ResetPassword(model));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
    }
}
