using e_commerce_server.src.Core.Modules.Product.Dto;
using e_commerce_server.src.Core.Modules.Product.Service;
using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Core.Modules.User;
using e_commerce_server.Src.Packages.HttpException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_server.src.Core.Api.V1.Controllers.Product
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class productsController : ControllerBase
    {
        private readonly MyDbContext _dbContext;
        private ProductService productService;
        public productsController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
            productService = new ProductService(dbContext);
        }
        //get all product
        [HttpGet]
        public IActionResult getAllProduct()
        {
            try
            {
                return Ok(productService.GetAllProduct());
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
        //edit product by product id
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult editProduct(ProductDto productDto, int id)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id");
                return Ok(productService.editProductByID(productDto, id,Convert.ToInt32(idClaim.Value)));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        //add new product
        [HttpPost]
        [Authorize]
        public IActionResult addNewProduct(ProductDto product)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id");
                return Ok(productService.addNewProduct(product, Convert.ToInt32(idClaim.Value)));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
        //get product by product id
        [HttpGet("{id}")]
        public IActionResult getProductById(int id)
        {
            try
            {
                return Ok(productService.GetProductById(id));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
    }
}
