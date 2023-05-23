using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace NewsWebsite.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "Admin,Moderator")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
