using e_commerce_server.Modes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<hanghoaVM> hanghoas = new List<hanghoaVM>();
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hanghoas);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var index = -1;
            
            foreach (hanghoaVM item in hanghoas)
            {
                if (item.id.ToString() == id)
                {
                    return Ok(new
                    {
                        success = true,
                        data = item
                    });
                }
                
            }
            return NotFound(new
            {
                success = false,
            });
        }
        [HttpPost]
        public IActionResult Create(hanghoa hanghoa)
        {
            var hanghoaNew = new hanghoaVM
            {
                id = Guid.NewGuid(),
                name = hanghoa.name,
                cost = hanghoa.cost,
            };
            hanghoas.Add(hanghoaNew);
            return Ok(new
            {
                success= true,
                data = hanghoas
            });
        }
    }
}
