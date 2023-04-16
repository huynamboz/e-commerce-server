using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Modules.Media;
using e_commerce_server.src.Core.Modules.User.Dto;
using e_commerce_server.src.Core.Modules.User.Service;
using e_commerce_server.src.Packages.HttpExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_server.src.Core.Api.V1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly UserService userService;
        private readonly MediaHandler mediaHandler;
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

                if (idClaim?.Value == null)
                {
                    throw new UnAuthorizedException();
                }

                return Ok(userService.GetUserById(Convert.ToInt32(idClaim.Value)));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpPut("me")]
        [Authorize]
        public IActionResult UpdateUser(UpdateUserDto model)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id");

                if (idClaim?.Value == null)
                {
                    throw new UnAuthorizedException();
                }

                return Ok(userService.UpdateUserById(model, Convert.ToInt32(idClaim.Value)));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        //get favorite products
        [HttpGet("me/favorite-products")]
        [Authorize]
        public IActionResult GetFavoriteProducts(int page = 1)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id");

                if (idClaim?.Value == null)
                {
                    throw new UnAuthorizedException();
                }

                return Ok(userService.GetFavoriteProducts(page, Convert.ToInt32(idClaim.Value)));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        //add product to favorite
        [HttpPost("me/favorite-products")]
        [Authorize]
        public IActionResult AddProductToFavorite(AddProductToFavoriteDto model)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id");

                if (idClaim?.Value == null)
                {
                    throw new UnAuthorizedException();
                }

                return Ok(userService.AddProductToFavorite(Convert.ToInt32(idClaim.Value), model));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        //remove product from favorite
        [HttpDelete("me/favorite-products/{id}")]
        [Authorize]
        public IActionResult RemoveProductFromFavorite(int id)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id");

                if (idClaim?.Value == null)
                {
                    throw new UnAuthorizedException();
                }

                return Ok(userService.RemoveProductFromFavorite(Convert.ToInt32(idClaim.Value), id));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpGet("{id}")]
        [Authorize] 
        public IActionResult GetUserById(int id) 
        {
            try
            {
                return Ok(userService.GetUserById(id));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
    }
}