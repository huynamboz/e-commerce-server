using e_commerce_server.src.Core.Common.Enum;
using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Modules.Product.Service;
using e_commerce_server.src.Core.Modules.Report.Service;
using e_commerce_server.src.Core.Modules.User.Service;
using e_commerce_server.src.Packages.HttpExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_server.src.Core.Api.V1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleEnum.ADMIN)]
    public class adminController : ControllerBase
    {
        private UserService userService;
        private ReportService reportService;
        private ProductService productService;
        public adminController(AppDbContext dbContext)
        {
            reportService = new ReportService(dbContext); 
            userService = new UserService(dbContext);
            productService = new ProductService(dbContext);
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(userService.GetAllUsers());
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpDelete("users/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                return Ok(userService.DeleteUserById(id));    
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpDelete("products/{id}")]
        public IActionResult DeleteProduct(int id) 
        {
            try
            {
                return Ok(productService.DeleteProductById(id));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpGet("reports")]
        public IActionResult GetReportsByUserId(int page = 1)
        {
            try
            {
                return Ok(reportService.GetReportsByUserId(page)); 
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
    }
}

