using e_commerce_server.Data;
using e_commerce_server.Modes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaisController : ControllerBase
    {
        private readonly MyDbContext _context;

        public LoaisController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet] 
        public IActionResult GetAll() { 
            var dsLoai = _context.Loais.ToList();
            return Ok(dsLoai);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var loai = _context.Loais.SingleOrDefault(loaiItem =>
            loaiItem.MaLoai == id);
            if (loai != null)
            return Ok(loai);
            else return NotFound();
        }
        [HttpPost]
        public IActionResult Createnew(LoaiModels model)
        {
            try
            {
                var loai = new Loai
                {
                    TenLoai = model.TenLoai,
                };
                _context.Add(loai);
                _context.SaveChanges();
                return Ok(loai);
            } catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateLoaiById(int id, LoaiModels model)
        {
            var loai = _context.Loais.SingleOrDefault(loaiItem =>
            loaiItem.MaLoai == id);
            if (loai != null)
            {
                loai.TenLoai = model.TenLoai;
                _context.SaveChanges();
                return Ok(loai);

            }
            else return NotFound();
        }
    }
}
