using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Packages.HttpExceptions;
using Microsoft.AspNetCore.Mvc;
using e_commerce_server.src.Core.Modules.Location.Service;

namespace e_commerce_server.src.Core.Api.V1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class locationController : ControllerBase
    {
        private readonly LocationService locationService;
        public locationController(AppDbContext dbContext)
        {
            locationService = new LocationService(dbContext);
        }

        [HttpGet("cities")]
        public IActionResult GetAllCities()
        {
            try
            {
                return Ok(locationService.GetAllCities());
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
                return Ok(locationService.GetDistrictsByCityId(id));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
    }
}
