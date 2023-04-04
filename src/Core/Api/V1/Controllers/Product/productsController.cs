using e_commerce_server.src.Core.Modules.Product.Dto;
using e_commerce_server.src.Core.Modules.Product.Service;
using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Packages.HttpException;
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
        //add new product
        [HttpGet]
        public IActionResult getAllProduct()
        {
            try
            {
                return Ok(productService.GetAllProduct());
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //edit product
        [HttpPut("{idProduct}")]
        public IActionResult editProduct(ProductDto productDto, int idProduct)
        {
            try
            {
                return Ok(productService.editProductByID(productDto, idProduct));
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult addNewProduct(ProductDto product,int userID)
        {
            try
            {
                return Ok(productService.addNewProduct(product,userID));
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
