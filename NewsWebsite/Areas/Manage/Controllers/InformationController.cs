using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsWebsite.Areas.Manage.ViewModels;
using NewsWebsite.DAL;
using NewsWebsite.Models;
using NewsWebsite.Helpers;
using System.Data;
using System.Numerics;
using Microsoft.VisualBasic;
using Information = NewsWebsite.Models.Information;

namespace NewsWebsite.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "Admin,Moderator")]
    public class InformationController : Controller
    {
        private readonly KatenDbContext _context;
        private readonly IWebHostEnvironment _env;

        public InformationController(KatenDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _context.Informations
                .Include(x => x.Categories)
                .Include(x => x.Authors)
                .Include(x => x.InformationImages);



            var model = PaginationList<Models.Information>.Create(query, page, 4);
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Authors = _context.Authors.ToList();


            return View();
        }

        [HttpPost]
        public IActionResult Create(Models.Information information)
        {
            if (!_context.Authors.Any(x => x.Id == information.AuthorId))
                ModelState.AddModelError("AuthorId", "Author not found");

            if (!_context.Categories.Any(x => x.Id == information.CategoryId))
                ModelState.AddModelError("CategoryId", "Category not found");

            _checkImageFiles(information.ImageFiles, information.PosterFile);

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Authors = _context.Authors.ToList();


                return View();
            }

            information.InformationImages = new List<InformationImage>();

            InformationImage poster = new InformationImage
            {
                Image = FileManager.Save(information.PosterFile, _env.WebRootPath, "uploads/informations"),
                PosterStatus = true,
            };

            information.InformationImages.Add(poster);


            foreach (var imgFile in information.ImageFiles)
            {
                InformationImage informationImage = new InformationImage
                {
                    Image = FileManager.Save(imgFile, _env.WebRootPath, "uploads/informations"),
                };
                information.InformationImages.Add(informationImage);
            }

            information.CreatedAt = DateTime.UtcNow.AddHours(4);


            _context.Informations.Add(information);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        private void _checkImageFiles(List<IFormFile> images, IFormFile posterFile)
        {
            if (posterFile == null)
                ModelState.AddModelError("PosterFile", "PosterFile is required");
            else if (posterFile.ContentType != "image/png" && posterFile.ContentType != "image/jpeg")
                ModelState.AddModelError("PosterFile", "Content type must be image/png or image/jpeg!");
            else if (posterFile != null && posterFile.Length > 2097152)
                ModelState.AddModelError("PosterFile", "File size must be less than 2MB!");

            if (images != null)
            {
                foreach (var imgFile in images)
                {
                    if (imgFile.ContentType != "image/png" && imgFile.ContentType != "image/jpeg")
                        ModelState.AddModelError("ImageFiles", "Content type must be image/png or image/jpeg!");

                    if (imgFile.Length > 2097152)
                        ModelState.AddModelError("ImageFiles", "File size must be less than 2MB!");
                }
            }
        }

        public IActionResult Edit(int id)
        {
            Models.Information information = _context.Informations.Include(x => x.InformationImages).FirstOrDefault(x => x.Id == id);

            if (information == null)
                return RedirectToAction("error", "dashboard");
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Authors = _context.Authors.ToList();

            return View(information);
        }

        [HttpPost]
        public IActionResult Edit(Models.Information information)
        {
            Information existInformation = _context.Informations.Include(x => x.InformationImages).FirstOrDefault(x => x.Id == information.Id);

            if (existInformation == null)
                return RedirectToAction("error", "dashboard");

            if (existInformation.CategoryId != information.CategoryId && !_context.Categories.Any(x => x.Id == information.CategoryId))
                ModelState.AddModelError("CategoryId", "Category not found!");

            if (existInformation.AuthorId != information.AuthorId && !_context.Authors.Any(x => x.Id == information.AuthorId))
                ModelState.AddModelError("AuthorId", "Author not found!");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Authors = _context.Authors.ToList();

                return View(existInformation);
            }

            if (information.PosterFile != null)
            {
                var poster = existInformation.InformationImages.FirstOrDefault(x => x.PosterStatus == true);
                var newPosterName = FileManager.Save(information.PosterFile, _env.WebRootPath, "uploads/informations");
                FileManager.Delete(_env.WebRootPath, "uploads/informations", poster.Image);
                poster.Image = newPosterName;
            }

            var removedFiles = existInformation.InformationImages.FindAll(x => x.PosterStatus == null && !information.InformationImageIds.Contains(x.Id));

            foreach (var item in removedFiles)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/informations", item.Image);
            }

            existInformation.InformationImages.RemoveAll(x => removedFiles.Contains(x));

            foreach (var imgFile in information.ImageFiles)
            {
                InformationImage informationImage = new InformationImage
                {
                    Image = FileManager.Save(imgFile, _env.WebRootPath, "uploads/informations"),
                };
                existInformation.InformationImages.Add(informationImage);
            }

            existInformation.CategoryId = information.CategoryId;
            existInformation.AuthorId = information.AuthorId;
            existInformation.Name = information.Name;
            existInformation.Title = information.Title;
            existInformation.Description = information.Description;

            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {

            Information information = _context.Informations.Include(x => x.InformationImages).FirstOrDefault(x => x.Id == id);
            var removedFiles = information.InformationImages.FindAll(x => x.PosterStatus == null);
            InformationImage informationImage = _context.InformationImages.FirstOrDefault(x => x.InformationId == id);
            if (information == null)
                return RedirectToAction("error", "dashboard");
            if (informationImage != null)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/informations", informationImage.Image);
            }
            foreach (var item in removedFiles)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/informations", item.Image);
            }

            information.InformationImages.RemoveAll(x => removedFiles.Contains(x));

            _context.Informations.Remove(information);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}

       
