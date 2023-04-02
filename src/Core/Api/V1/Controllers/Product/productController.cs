using e_commerce_server.src.Core.Modules.Product.Service;
using e_commerce_server.Src.Core.Database.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_server.src.Core.Api.V1.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        private readonly MyDbContext _dbContext;
        private ProductService productService;
        public productController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
            productService = new ProductService(dbContext);
        }
        [HttpGet]
        public IActionResult getAllProduct()
        {
            return Ok(productService.GetAllProduct());
        }
    }
}
