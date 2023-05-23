using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsWebsite.Areas.Manage.ViewModels;
using NewsWebsite.Models;
using System.Data;

namespace NewsWebsite.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> CreateRole()
        {
            IdentityRole role1 = new IdentityRole("Moderator");
            IdentityRole role2 = new IdentityRole("Admin");

            await _roleManager.CreateAsync(role1);
            await _roleManager.CreateAsync(role2);

            return Ok();
        }

        public async Task<IActionResult> CreateAdmin()
        {
            AppUser admin = new AppUser
            {
                UserName = "Yusif_Admin",
                FullName = "Yusif Huseynzade",
            };

            await _userManager.CreateAsync(admin, "Yusif12345");

            await _userManager.AddToRoleAsync(admin, "Admin");

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel loginVM, string returnUrl)
        {
            AppUser user = await _userManager.FindByNameAsync(loginVM.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "Username or Passwrod is incorrect!");
                return View();
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("Admin") && !roles.Contains("Moderator"))
            {
                ModelState.AddModelError("", "Username or Passwrod is incorrect!");
                return View();
            }


            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.IsPersist, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "5 dəqiqə sonra təkrar cəhd edin!");
                return View();
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Passwrod is incorrect!");
                return View();
            }

            if (returnUrl != null)
                return Redirect(returnUrl);

            return RedirectToAction("index", "dashboard");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }

    }
}
