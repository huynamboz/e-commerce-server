using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Modules.Media;
using e_commerce_server.src.Core.Modules.User.Dto;
using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Modules.User.Service;
using e_commerce_server.src.Packages.HttpExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_server.src.Core.Api.V1.Controllers.User
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private UserService userService;
        private MediaHandler mediaHandler;
        private UserData userData;
        public usersController(MyDbContext dbContext)
        {
            userService = new UserService(dbContext);
            mediaHandler = new MediaHandler();
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetUser()
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id");

                return Ok(userService.GetUserById(Convert.ToInt32(idClaim.Value)));

            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpPut("me")]
        [Authorize] 
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserDto updateDto, List<IFormFile> file)
        {
            try
            {
                List<string> filePaths = await mediaHandler.Validate(file, 1).Save();

                string? filePath = filePaths.Count > 0 ? filePaths[0] : null;

                var idClaim = HttpContext.User.FindFirst("id");

                return Ok(userService.UpdateUserById(filePath, updateDto, Convert.ToInt32(idClaim.Value)));
            } 
            catch (HttpException ex) 
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
    }
}