using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsWebsite.DAL;
using NewsWebsite.Models;
using System.Data;

namespace NewsWebsite.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "Admin,Moderator")]
    public class CategoryController : Controller
    {
        private readonly KatenDbContext _context;

        public CategoryController(KatenDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            var model = _context.Categories.Include(x => x.Informations).Skip((page - 1) * 6).Take(6).ToList();
            ViewBag.Page = page;
            ViewBag.TotalPage = (int)Math.Ceiling(_context.Categories.Count() / 6d);

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (_context.Categories.Any(x => x.Name == category.Name))
            {
                ModelState.AddModelError("Name", "This name has already exist");
                return View();
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null)
                return RedirectToAction("error", "dashboard");

            return View(category);
        }



        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (_context.Categories.Any(x => x.Name == category.Name && x.Id != category.Id))
            {
                ModelState.AddModelError("Name", "This name has already exist");
                return View();
            }

            Category existCategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);

            existCategory.Name = category.Name;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.Include(x => x.Informations).FirstOrDefault(x => x.Id == id);

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            Category existCategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);

            if (!_context.Informations.Any(x => x.CategoryId == category.Id))
            {
                _context.Categories.Remove(existCategory);
                _context.SaveChanges();
            }

            return RedirectToAction("index");
        }
    }
}
