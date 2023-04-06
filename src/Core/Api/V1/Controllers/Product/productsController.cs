using e_commerce_server.src.Core.Modules.Product.Dto;
using e_commerce_server.src.Core.Modules.Product.Service;
using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Packages.HttpException;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult getAllProduct(int page = 1)
        {
            try
            {
                return Ok(productService.GetAllProducts(page));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        //get product by id
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

        [HttpGet("my-products")]
        [Authorize]
        public IActionResult getMyProducts(int page = 1)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id");
                return Ok(productService.GetProductsByUserId(page, Convert.ToInt32(idClaim.Value)));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        //add new product
        [HttpPost("my-products")]
        [Authorize]
        public IActionResult AddProduct(AddProductDto product)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id");

                return Ok(productService.AddProduct(product, Convert.ToInt32(idClaim.Value)));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        //edit product by id
        [HttpPut("my-products/{id}")]
        [Authorize]
        public IActionResult editProduct(AddProductDto productDto, int id)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id");
                return Ok(productService.EditProductById(productDto, id,Convert.ToInt32(idClaim.Value)));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
    }
}
