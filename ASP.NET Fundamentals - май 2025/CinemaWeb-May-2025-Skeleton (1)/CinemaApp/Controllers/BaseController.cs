using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {


        protected bool IsUserAuthenticated()
        {
            //Ako identiti e null return false
            return this.User.Identity?.IsAuthenticated ?? false;
        }


        protected string? GetUserId()
        {
            if (IsUserAuthenticated())
            {
                return User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return null;
        }

    }
}
