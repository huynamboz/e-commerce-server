using e_commerce_server.Data;
using e_commerce_server.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Authorize]
        public IActionResult getAllProduct()
        {
            var data = _context.Products.ToList();
           
            return Ok(data);
        }
        [HttpGet("{id}")]
        public IActionResult getProductById(string id)
        {
            List<string> listThumbnailUrl = new List<string>();
            var data = _context.Products.SingleOrDefault(item => item.id.ToString() == id);
            var listThumbnail = _context.Thumbnails.Where(item => item.product_id == id).ToList();
            foreach(var thumbnail in listThumbnail)
            {
                listThumbnailUrl.Add(thumbnail.FilePath);
            }
            return Ok(new
            {
                data = data,
                thumbnails = listThumbnailUrl
            });
        }
        [HttpGet("user/{user_id}")]
        [Authorize]
        public IActionResult getProductByUserId(string user_id)
        {
            var data = _context.Products.Where(item => item.user_id.ToString() == user_id);
            return Ok(new
            {
                data = data,
            });
        }
        //api thêm sản phẩm mới
        [HttpPost]
        [Authorize]
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
        //api giúp thay đổi avata của product
        [HttpPut("{id}/thumbnail")]
        [Authorize]
        public IActionResult changeMainThumbnail(string id,string imgPath) {
            var product = _context.Products.FirstOrDefault(item => item.id.ToString() == id);
            if(product == null)
            {
                return NotFound();
            }
            product.thumbnail_url = imgPath;
            _context.SaveChanges();
            return Ok(product);
        }
    }
}
