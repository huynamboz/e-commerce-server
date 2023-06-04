using e_commerce_server.src.Core.Modules.Media;
using e_commerce_server.src.Core.Modules.Media.Service;
using e_commerce_server.src.Packages.HttpExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_server.src.Core.Api.V1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class uploadController : ControllerBase
    {
        private readonly MediaHandler mediaHandler;
        private readonly MediaService mediaService;

        public uploadController()
        {
            mediaHandler = new MediaHandler();
            mediaService = new MediaService();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            try
            {
                List<string> filePaths = await mediaHandler.Validate(files).Save();

                var idClaim = HttpContext.User.FindFirst("id");

                return Created("CREATED", mediaService.UploadMany(filePaths, "BadSuperMarket"));
            } catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
    }
}
