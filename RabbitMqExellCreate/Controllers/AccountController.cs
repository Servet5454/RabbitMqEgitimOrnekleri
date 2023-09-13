using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RabbitMqExellCreate.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Email,string Password)
        {
            var hasUser = new IdentityUser();
            hasUser.Email = Email;
           
            if(hasUser != null)
            {
                _userManager.CreateAsync(hasUser, Password);
               
            }
            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, Password, true, false);
            if(!signInResult.Succeeded)
            {
                return View();
            }
            return RedirectToAction(nameof(HomeController.Index), "home");
        }
    }
}
