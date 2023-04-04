using e_commerce_server.Src.Packages.HttpException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_server.Src.Core.Api.V1.Controllers.User
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
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
    }
}