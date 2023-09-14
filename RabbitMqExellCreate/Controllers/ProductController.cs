using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMqExellCreate.Models;
using RabbitMqExellCreate.Services;
using Shared;

namespace RabbitMqExellCreate.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbcontext _context;
        private readonly RabbitMqPublisher _rabbitMqPublisher;
        public ProductController(AppDbcontext context, RabbitMqPublisher rabbitMqPublisher)
        {
            _context = context;
            _rabbitMqPublisher = rabbitMqPublisher;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateProductExel()
        {

            var user = await _context.Users.FirstOrDefaultAsync(P=>P.Id ==1.ToString());
            var fileName = $"product-exel-{Guid.NewGuid().ToString().Substring(1, 10)}";
            Random rnd = new Random();
            int sayi = rnd.Next(1, 30);

            UserFile userfile = new()
            {
                
                UserId = user.Id.ToString(),
                FileName = fileName,
                FileStatus = FileStatus.Creating
            };
            await _context.UserFiles.AddAsync(userfile);
            await _context.SaveChangesAsync();
            //rabbitmq ya mesaj yollanıcak
            _rabbitMqPublisher.Publish(new Shared.CreateExcelMessage() { FileId = userfile.Id});
            TempData["StartCreatingExel"] = true;
            return RedirectToAction(nameof(Files));
        }

        public async Task<IActionResult> Files()
        {

            return View(await _context.UserFiles.Where(p=>p.UserId !=null).OrderByDescending(p=>p.CreatedDate).ToListAsync());
        }
    }
}
