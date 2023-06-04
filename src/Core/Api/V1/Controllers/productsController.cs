using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Modules.Product.Dto;
using e_commerce_server.src.Core.Modules.Product.Service;
using e_commerce_server.src.Core.Modules.Report.Service;
using e_commerce_server.src.Core.Modules.Review.Dto;
using e_commerce_server.src.Core.Modules.Review.Service;
using e_commerce_server.src.Packages.HttpExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using e_commerce_server.src.Core.Modules.Report.Dto;
using Microsoft.AspNetCore.Http;

namespace e_commerce_server.src.Core.Api.V1.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class productsController : ControllerBase
    {
        private readonly ProductService productService;
        private readonly ReviewService reviewService;
        private readonly ReportService reportService;
        public productsController(AppDbContext dbContext)
        {
            productService = new ProductService(dbContext);
            reviewService = new ReviewService(dbContext);
            reportService = new ReportService(dbContext);
        }

        //get all product
        [HttpGet("[controller]")]
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
        [HttpGet("[controller]/{id}")]
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

        [HttpGet("users/me/[controller]")]
        [Authorize]
        public IActionResult GetMyProducts(int page = 1)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id")?.Value;

                return Ok(productService.GetProductsByUserId(page, Convert.ToInt32(idClaim)));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        //add new product
        [HttpPost("users/me/[controller]")]
        [Authorize]
        public IActionResult AddProduct(AddProductDto productDto)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id")?.Value;

                return Ok(productService.AddProduct(productDto, Convert.ToInt32(idClaim)));
            } catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
        //update product by id
        [HttpPut("users/me/[controller]/{id}")]
        [Authorize]
        public IActionResult EditProduct(AddProductDto productDto, int id)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id")?.Value;

                return Ok(productService.EditProductById(productDto, id, Convert.ToInt32(idClaim)));
            } catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        //delete product by id
        [HttpDelete("users/me/[controller]/{id}")]
        [Authorize]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id")?.Value;

                return Ok(productService.DeleteProductById(Convert.ToInt32(idClaim), id));
            } catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpGet("users/{id}/[controller]")]
        public IActionResult GetProductsByUser(int id, int page = 1)
        {
            try
            {
                return Ok(productService.GetProductsByUserId(page, id));
            } catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpPost("[controller]/{productId}/reviews")]
        [Authorize]
        public IActionResult CreateReview(int productId, ReviewProductDto reviewDto)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id")?.Value;

                return Ok(reviewService.CreateOrUpdateReview(productId, Convert.ToInt32(idClaim), reviewDto));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpPut("[controller]/{productId}/reviews")]
        [Authorize]
        public IActionResult UpdateReview(int productId, ReviewProductDto reviewDto)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id")?.Value;

                return Ok(reviewService.CreateOrUpdateReview(productId, Convert.ToInt32(idClaim), reviewDto));
            } catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpGet("[controller]/{id}/prices-comparison")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(productService.GetPricesComparison(id));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpDelete("[controller]/{id}/reviews")]
        [Authorize]
        public IActionResult DeleteReview(int id)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id")?.Value;

                return Ok(reviewService.DeleteReview(id, Convert.ToInt32(idClaim)));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpPost("[controller]/{productId}/reports")]
        [Authorize]
        public IActionResult ReportProduct(int productId, ReportProductDto reportDto)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id")?.Value;

                return Ok(reportService.CreateOrUpdateReport(productId, Convert.ToInt32(idClaim), reportDto));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpPut("[controller]/{productId}/reports")]
        [Authorize]
        public IActionResult UpdateReport(int productId, ReportProductDto reportDto)
        {
            try
            {
                var idClaim = HttpContext.User.FindFirst("id")?.Value;

                return Ok(reportService.CreateOrUpdateReport(productId, Convert.ToInt32(idClaim), reportDto));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpGet("[controller]/search")]
        public IActionResult SearchProducts(string name, int district_id, int city_id, int category, string price, string time, int page = 1)
        {
            try
            {
                return Ok(productService.SearchProducts(name, district_id, city_id, category, price, time, page));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpGet("[controller]/categories")]
        public IActionResult GetCategories()
        {
            try
            {
                return Ok(productService.GetCategories());
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpGet("[controller]/categories/{id}/products")]
        public IActionResult GetProductByCategories(int id, int page = 1)
        {
            try
            {
                return Ok(productService.GetAllProductsByCategories(id, page));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
    }
}
