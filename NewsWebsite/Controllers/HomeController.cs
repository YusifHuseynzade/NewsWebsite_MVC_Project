using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsWebsite.DAL;
using NewsWebsite.Models;
using NewsWebsite.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace NewsWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly KatenDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(KatenDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            //AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            HomeViewModel viewModel = new HomeViewModel
            {
                //Travels = _context.Informations.Include(x => x.Categories).Include(x => x.InformationImages).Include(x => x.Authors).Where(x => x.IsTravel).Take(6).ToList(),
                //Healths = _context.Informations.Include(x => x.Categories).Include(x => x.InformationImages).Include(x => x.Authors).Where(x => x.IsHealth).Take(6).ToList(),
                //Politics = _context.Informations.Include(x => x.Categories).Include(x => x.InformationImages).Include(x => x.Authors).Where(x => x.IsPolitic).Take(6).ToList(),
                //Technologies = _context.Informations.Include(x => x.Categories).Include(x => x.InformationImages).Include(x => x.Authors).Where(x => x.IsTechnology).Take(6).ToList(),
                //Foods = _context.Informations.Include(x => x.Categories).Include(x => x.InformationImages).Include(x => x.Authors).Where(x => x.IsFood).Take(6).ToList(),
                //Architectures = _context.Informations.Include(x => x.Categories).Include(x => x.InformationImages).Include(x => x.Authors).Where(x => x.IsArchitecture).Take(6).ToList(),
                Informations = _context.Informations.Include(x => x.Categories).Include(x => x.InformationImages).Include(x => x.Authors).OrderByDescending(x => x.CreatedAt).ToList(),
                AppUsers = _context.AppUsers.ToList()

            };
            return View(viewModel);
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Error()
        {
            return View();
        }
    }
}