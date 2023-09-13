using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMqExellCreate.Models;

namespace RabbitMqExellCreate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly AppDbcontext _context;

        public FilesController(AppDbcontext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> upload(IFormFile file,int fileId)
        {
            if (file is not { Length: > 0 }) return BadRequest();
            var enBuyukIdliUserFile = _context.UserFiles.OrderByDescending(p => p.Id).FirstOrDefault();

            var userFile = await _context.UserFiles.FirstAsync(p => p.Id == enBuyukIdliUserFile.Id);//todo burada sıkıntı var
            var filePath = userFile.FileName + Path.GetExtension(file.FileName);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", filePath);

            using FileStream stream = new(path, FileMode.Create);
            await file.CopyToAsync(stream);

            userFile.CreatedDate = DateTime.Now;
            userFile.FilePath = filePath;
            userFile.FileStatus = FileStatus.completed;
            await _context.SaveChangesAsync();

            //signalR ile notification oluşturulacak
            return Ok();
        }
    }
}
