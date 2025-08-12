using Microsoft.AspNetCore.Mvc;
using RecipeSharingPlatform.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace RecipeSharingPlatform.Web.Controllers; //tui da ne me bugne
public class HomeController : BaseController
{
   

    [AllowAnonymous]
    public IActionResult Index()
    {
        if (IsUserAuthenticated())
        {
            return RedirectToAction("Index", "Recipe");
        }
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}