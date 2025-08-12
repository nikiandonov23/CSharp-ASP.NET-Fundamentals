using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingPlatform.ViewModels;
using System.Diagnostics;

namespace RecipeSharingPlatform.Web.Controllers;


[AllowAnonymous]
public class HomeController : BaseController
{
    

    public async Task<IActionResult> Index()
    {
        var userId = GetUserId();

        if (IsUserAuthenticated())
        {
            return RedirectToAction("Index", "Recipe");
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