using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsWebsite.DAL;
using NewsWebsite.Models;
using NewsWebsite.ViewModels;
using System.Data;
using NewsWebsite.Areas.Manage.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewsWebsite.Controllers
{
    public class NewsController : Controller
    {
        private readonly KatenDbContext _context;

        public NewsController(KatenDbContext context)
        {
            _context = context;
            

        }

        //public async Task<IActionResult> Detail(int id)
        //{
        //    Information information = _context.Informations
        //        .Include(x => x.Categories)
        //        .Include(x => x.Authors)
        //        .Include(x => x.InformationImages)
        //        .FirstOrDefault(x => x.Id == id);

        //    if (information == null)
        //    {
        //        TempData["error"] = "Xəbər yoxdur";
        //        return RedirectToAction("index", "home");
        //    }

        //    NewsDetailViewModel detailVM = new NewsDetailViewModel
        //    {
        //        Information = information,
        //        Informations = _context.Informations.Include(x => x.Categories).Include(x => x.Authors).Include(x => x.InformationImages).Take(3).ToList(),
        //        RelatedInformations = _context.Informations.Include(x => x.Categories)
        //       .Include(x => x.Authors)
        //       .Include(x => x.InformationImages)
        //       .Where(x => x.Categories == information.Categories)
        //       .Take(6).ToList(),
        //    };

        //    if (information == null)
        //        return NotFound();

        //    return View(detailVM);
        //}

        public IActionResult Detail()
        {
            return View();
        }
    }
}
