
using e_commerce_server.src.Core.Modules.User.Dto;
using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Core.Modules.User.Service;
using e_commerce_server.Src.Packages.HttpException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_server.Src.Core.Api.V1.Controllers.User
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly MyDbContext _dbContext;
        private UserService userService;
        private UserData userData;
        public usersController(MyDbContext dbContext)
        {
            userData = new UserData();
            _dbContext = dbContext;
            userService = new UserService(dbContext);
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Users()
        {
            var idClaim = HttpContext.User.FindFirst("id");
            if (idClaim != null)
            {
                return Ok(idClaim.Value);
            }
            return Ok();
        }

        [HttpPut("me")]
        [Authorize] 
        public IActionResult UpdateUser(UpdateUserDto updateDto)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id");
                return Ok(userService.UpdateUserById(updateDto, Convert.ToInt32(idClaim.Value)));
            } 
            catch (HttpException ex) 
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
    }
}