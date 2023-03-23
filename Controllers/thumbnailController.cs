using e_commerce_server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class thumbnailController : ControllerBase
    {
        private readonly MyDbContext _context;
        public thumbnailController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public IActionResult getProductById(string id)
        {
            var listThumbnail = _context.Thumbnails.Where(item => item.product_id == id).ToList();
            return Ok(listThumbnail);
        }
        [HttpPost("{productID}/upload")]
        public async Task<IActionResult> UploadThumbnailFile(List<IFormFile> files, string productID)
        {
            try
            {
                List<thumbnail> lsFm = new List<thumbnail>();
                if (files == null || files.Count == 0)
                    return BadRequest("Invalid file");
                Guid guid = Guid.NewGuid();
                foreach (var file in files)
                {
                     var fileName = guid + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var fileModel = new thumbnail
                {
                    FilePath = "/uploads/" + fileName,
                    product_id= productID,
                    // các thuộc tính khác liên quan đến file
                };
                    lsFm.Add(fileModel);
                }
               

                // Lưu fileModel vào database
                _context.Thumbnails.AddRange(lsFm);
                await _context.SaveChangesAsync();

                return Ok(lsFm);
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    message = ex.ToString()
                });
            }
        }
    }
}
