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
            var data = _context.Products.ToList();
            return Ok(data);
        }
        [HttpPost]
        public IActionResult postProduct(productModels item)
        {
            var newProduct = new product
            {
                user_id = item.user_id,
                name= item.name,
                description= item.description,
                category_id= item.category_id,
                discount = item.discount,
                created_at= DateTime.Now.ToString("yyyyMMdd"),
                price = item.price,
                thumbnail_url= item.thumbnail_url,
            };
            _context.Add(newProduct);
            _context.SaveChanges();
            return Ok(newProduct);
        }
    }
}
