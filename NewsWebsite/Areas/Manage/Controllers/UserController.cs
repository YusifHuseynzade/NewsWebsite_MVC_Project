using NewsWebsite.Areas.Manage.ViewModels;
using NewsWebsite.DAL;
using NewsWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace NewsWebsite.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "Admin,Moderator")]
    public class UserController : Controller
    {
        private readonly KatenDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;
        public UserController(KatenDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _env = env;
        }

        public IActionResult Index(int page = 1)
        {
            var model = _context.AppUsers.Skip((page - 1) * 8).Take(8).ToList();
            ViewBag.Page = page;
            ViewBag.TotalPage = (int)Math.Ceiling(_context.AppUsers.Count() / 8d);

            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Roles = _roleManager.Roles.ToList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(AdminCreateViewModel createdAdminAndMember)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _roleManager.Roles.ToList();
                return View(createdAdminAndMember);
            }

            if (await _userManager.FindByEmailAsync(createdAdminAndMember.Email) != null)
            {
                ModelState.AddModelError("Email", "This email is already exist!");
                ViewBag.Roles = _roleManager.Roles.ToList();
                return View(createdAdminAndMember);
            }

            if (await _userManager.FindByNameAsync(createdAdminAndMember.UserName) != null)
            {
                ViewBag.Roles = _roleManager.Roles.ToList();
                ModelState.AddModelError("Username", "This Username is already exist!");
                return View(createdAdminAndMember);
            }

            AppUser AdminUser = new AppUser
            {
                FullName = createdAdminAndMember.FullName,
                UserName = createdAdminAndMember.UserName,
                Email = createdAdminAndMember.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(AdminUser, createdAdminAndMember.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    ViewBag.Roles = _roleManager.Roles.ToList();
                    return View(createdAdminAndMember);
                }
            }

            foreach (var roleName in createdAdminAndMember.RoleName)
            {
                var role = _roleManager.Roles.FirstOrDefault(x => x.Name == roleName);
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(AdminUser, role.Name);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("index", "user");
        }

        public async Task<IActionResult> Delete(string userId)
        {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                    return NotFound();
                await _userManager.DeleteAsync(user);

            return RedirectToAction("index", "user");

        }
    }
}
