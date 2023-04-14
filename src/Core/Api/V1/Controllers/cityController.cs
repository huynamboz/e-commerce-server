using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Modules.Media;
using e_commerce_server.src.Packages.HttpExceptions;
using Microsoft.AspNetCore.Mvc;
using e_commerce_server.src.Core.Modules.City.Service;

namespace e_commerce_server.src.Core.Api.V1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class cityController : ControllerBase
    {
        private CityService cityService;
        public cityController(MyDbContext dbContext)
        {
            cityService = new CityService(dbContext);
        }

        [HttpGet("cities")]
        public IActionResult GetAllCities()
        {
            try
            {
                return Ok(cityService.GetAllCities());
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpGet("cities/{id}/districts")]
        public IActionResult GetDistricts(int id)
        {
            try
            {
                return Ok(cityService.GetDistrictsByCityId(id));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
    }
}
