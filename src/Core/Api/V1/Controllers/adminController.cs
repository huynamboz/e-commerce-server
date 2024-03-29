﻿using e_commerce_server.src.Core.Common.Enum;
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
        public IActionResult GetReports(int page = 1)
        {
            try
            {
                return Ok(reportService.GetReports(page)); 
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpDelete("reports/{productId}/accept")]
        public IActionResult AcceptReports(int productId)
        {
            try
            {
                return Ok(reportService.DeleteProduct(productId)); 
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpDelete("products/{productId}/reports/{userId}/reject")]
        public IActionResult RejectReport(int productId, int userId)
        {
            try
            {
                return Ok(reportService.DeleteReport(productId, userId));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpPatch("users/{id}")]
        public IActionResult UnbanUser(int id)
        {
            try
            {
                return Ok(userService.UnbanUserById(id));
			}
			catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }

        [HttpGet("products/pending-products")]
        public IActionResult GetAllPendingProducts(int page = 1)
        {
            try
            {
                return Ok(productService.GetAllPendingProducts(page));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
		
        [HttpPatch("products/{id}/accept")]
        public IActionResult AcceptPublishProduct(int id)
        {
            try
            {
                return Ok(productService.AcceptPublishProduct(id));
            }
            catch (HttpException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Response);
            }
        }
    }
}
