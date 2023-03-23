using e_commerce_server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e_commerce_server.Model;
using System.IO;

namespace e_commerce_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class uploadFileController : ControllerBase
    {
        private readonly MyDbContext _context;
        public uploadFileController(MyDbContext context)
        {
            _context = context;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("Invalid file");
                Guid guid= Guid.NewGuid();
                var fileName = guid + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var fileModel = new FileModel
                {
                    FileName = fileName,
                    FilePath = "/uploads/" + fileName,
                    ContentType = file.ContentType,
                    Size = file.Length
                    // các thuộc tính khác liên quan đến file
                };

                // Lưu fileModel vào database
                _context.Files.Add(fileModel);
                await _context.SaveChangesAsync();

                return Ok(fileModel.FilePath);
            } catch(Exception ex)
            {
                return NotFound( new
                {
                    message = ex.ToString()
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
            var fileModel = await _context.Files.FindAsync(id);
            if (fileModel == null)
                return NotFound();

            var fileStream = new FileStream(fileModel.FilePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, fileModel.ContentType, fileModel.FileName);
        }
    }
}
