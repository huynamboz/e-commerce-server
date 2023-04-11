using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Modules.Media;
using e_commerce_server.src.Core.Modules.Product.Dto;
using e_commerce_server.src.Core.Modules.Product.Service;
using e_commerce_server.src.Packages.HttpExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_server.src.Core.Api.V1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class productsController : ControllerBase
    {
        private ProductService productService;
        private MediaHandler mediaHandler;
        public productsController(MyDbContext dbContext)
        {
            productService = new ProductService(dbContext);
            mediaHandler = new MediaHandler();
        }

        //get all product
        [HttpGet]
        public IActionResult GetAllProducts(int page = 1)
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
        public IActionResult GetProductById(int id)
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
        public IActionResult GetMyProducts(int page = 1)
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
        public async Task<IActionResult> AddProduct([FromForm] AddProductDto productDto, List<IFormFile> files)
        {
            try
            {
                List<string> filePaths = await mediaHandler.Validate(files).Save();

                var idClaim = HttpContext.User.FindFirst("id");

                return Ok(productService.AddProduct(filePaths, productDto, Convert.ToInt32(idClaim.Value)));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpGet("my-products/{id}")]
        [Authorize]
        public IActionResult GetProductByUserId(int id)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id");

                return Ok(productService.GetProductByUserId(Convert.ToInt32(idClaim.Value), id));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        //edit product by id
        [HttpPut("my-products/{id}")]
        [Authorize]
        public async Task<IActionResult> EditProduct([FromForm] AddProductDto productDto, List<IFormFile> files, int id)
        {
            try
            {
                List<string> filePaths = await mediaHandler.Validate(files).Save();

                var idClaim = HttpContext.User.FindFirst("id");

                return Ok(productService.EditProductById(filePaths, productDto, id, Convert.ToInt32(idClaim.Value)));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        //delete product by id
        [HttpDelete("my-products/{id}")]
        [Authorize]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id");

                return Ok(productService.DeleteProductById(Convert.ToInt32(idClaim.Value), id));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
    }
}
