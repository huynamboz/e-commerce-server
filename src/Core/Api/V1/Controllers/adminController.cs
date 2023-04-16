using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Modules.User.Service;
using e_commerce_server.src.Packages.HttpExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_server.src.Core.Api.V1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class adminController : ControllerBase
    {
        private UserService userService;
        public adminController(MyDbContext dbContext)
        {
            userService = new UserService(dbContext);
        }

        [HttpGet("/users")]
        public IActionResult GetAllUser()
        {
            try
            {
                var roleIdClaim = HttpContext.User.FindFirst("role_id");

                return Ok(userService.GetAllUsers(Convert.ToInt32(roleIdClaim.Value)));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpPatch("/users/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var roleIdClaim = HttpContext.User.FindFirst("role_id"); 

                return Ok(userService.DeleteUserById(Convert.ToInt32(roleIdClaim.Value), id));    
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
    }
}
