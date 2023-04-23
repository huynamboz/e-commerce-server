using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Modules.Product.Service;
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
        private ProductService productService;
        public adminController(MyDbContext dbContext)
        {
            userService = new UserService(dbContext);
            productService = new ProductService(dbContext);
        }

        [HttpGet("/users")]
        public IActionResult GetAllUser()
        {
            try
            {
                var roleIdClaim = HttpContext.User.FindFirst("role_id")?.Value;

                return Ok(userService.GetAllUsers(Convert.ToInt32(roleIdClaim)));
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
                var roleIdClaim = HttpContext.User.FindFirst("role_id")?.Value; 

                return Ok(userService.DeleteUserById(Convert.ToInt32(roleIdClaim), id));    
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpDelete("/products/{id}")]
        public IActionResult DeleteProduct(int id) 
        {
            try
            {
                var roleIdClaim = HttpContext.User.FindFirst("role_id")?.Value;

                return Ok(productService.DeleteProductByProductId(Convert.ToInt32(roleIdClaim), id));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

    }
}
