using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Horizons.Web.Controllers
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
                //Ako e Autenticated returns userId
                return User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return null;
        }

    }
}
