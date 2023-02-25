using e_commerce_server.Data;
using e_commerce_server.Modes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        private readonly MyDbContext _context;
        public productController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult getAllProduct()
        {
            var data = _context.HangHoas.ToList();
            return Ok(data);
        }
        [HttpPost]
        public IActionResult putProduct(productModels item)
        {
            var hang = new HangHoa
            {
                TenHh= item.TenHh,
                MoTa = item.MoTa,
                DonGia = item.DonGia,
                MaLoai = item.MaLoai,
            };
            _context.Add(hang);
            _context.SaveChanges();
            return Ok(hang);
        }
    }
}
