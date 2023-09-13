using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace RabbitMqExellCreate.Models
{
    public class AppDbcontext : IdentityDbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {


        }

       

        public DbSet<UserFile> UserFiles { get; set; }
    }
}
