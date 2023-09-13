using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMqExellCreate.Models;
using RabbitMqExellCreate.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(sp => new ConnectionFactory()
{
    Uri = new Uri(builder.Configuration.GetConnectionString("RabbitMQ")),
    DispatchConsumersAsync = true
});
builder.Services.AddSingleton<RabbitMqClientsService>();
builder.Services.AddSingleton<RabbitMqPublisher>();
builder.Services.AddDbContext<AppDbcontext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServer"]);
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppDbcontext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
using (var scope = app.Services.CreateScope())
{

    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbcontext>();
    //Resolve ASP .NET Core Identity with DI help
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    appDbContext.Database.Migrate();


    if (!appDbContext.Users.Any())
    {
        await userManager.CreateAsync(new IdentityUser()
        {
            UserName = "deneme",
            Email = "deneme@gmail.com"
        }, "ser123vet");
        await userManager.CreateAsync(new IdentityUser()
        {
            UserName = "deneme2",
            Email = "deneme2@gmail.com"
        }, "ser123vet");
    }

}

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
