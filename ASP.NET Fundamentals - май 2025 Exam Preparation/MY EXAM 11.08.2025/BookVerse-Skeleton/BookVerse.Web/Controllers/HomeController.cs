using System.Diagnostics;
using BookVerse.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookVerse.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        

        public IActionResult Index()
        {
            if (IsUserAuthenticated())
            {
               return RedirectToAction("Index","Book");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
